using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;
using System.IO;

public class SkeletonBin  {
	public const int TIMELINE_SCALE = 0;
	public const int TIMELINE_ROTATE = 1;
	public const int TIMELINE_TRANSLATE = 2;
	public const int TIMELINE_ATTACHMENT = 3;
	public const int TIMELINE_COLOR = 4;
	public const int TIMELINE_EVENT = 5;
	public const int TIMELINE_DRAWORDER = 6;
	public const int TIMELINE_FFD = 7;
	
	public const int CURVE_LINEAR = 0;
	public const int CURVE_STEPPED = 1;
	public const int CURVE_BEZIER = 2;
	
	Color m_tempColor = Color.white;
	
	int m_index = 0;
	AttachmentLoader m_attachmentLoader;
	bool m_nonessential;
	byte[] m_temp;
	
	public SkeletonBin (Atlas atlas)
	{
		m_attachmentLoader = new AtlasAttachmentLoader(atlas);
	}
	
	
	public SkeletonData LoadSkeletonBinary(byte[] binaryTemp)
	{
		SkeletonData skeletonData = new SkeletonData();
		skeletonData.name = null;
		
		m_temp = binaryTemp;
		
		//Version
		ReadString ();	
		//Hash
		ReadString ();
		//Width
		ReadFloat ();
		//Height
		ReadFloat ();
		//Nonessential
		m_nonessential = ReadBoolean ();
		
		//BoneNum
		int boneNum = ReadInt ();
		for(int i=0;i<boneNum;i++)
		{
			//Bone
			string name = ReadString ();
			BoneData parent = null;
			//index
			int parentIndex = ReadInt ()-1;
			if(parentIndex != -1) parent = skeletonData.bones[parentIndex];
			
			BoneData boneData = new BoneData(name,parent);
			//X
			boneData.x =ReadFloat ();
			//Y
			boneData.y = ReadFloat ();
			//scaleX
			boneData.ScaleX = ReadFloat ();
			//scaleY
			boneData.ScaleY= ReadFloat ();
			//rotation
			boneData.Rotation = ReadFloat ();
			//length
			boneData.Length = ReadFloat ();
			//inheritScale 
			boneData.InheritScale = ReadBoolean();
			//inheritRotation
			boneData.InheritRotation = ReadBoolean();
			//Color
			if(m_nonessential)
			{
				ReadColor ();
			}
			skeletonData.AddBone (boneData);
		}
		
		//SlotsNum
		int slotsNum = ReadInt ();
		for(int i = 0;i<slotsNum;i++)
		{
			string slotName = ReadString ();
			int boneIndex = ReadInt ();
			BoneData boneData = skeletonData.bones[boneIndex];
			SlotData slotData = new SlotData(slotName,boneData);
			
			Color color = ReadColor ();   //Color
			slotData.R = color.r;
			slotData.G = color.g;
			slotData.B = color.b;
			slotData.A = color.a;
			
			slotData.AttachmentName = ReadString ();
			slotData.AdditiveBlending = ReadBoolean ();
			
			skeletonData.AddSlot (slotData);
		}
		
		//Default skin
		Skin defaultSkin = readSkin("default",m_nonessential);
		if(defaultSkin != null)
		{
			skeletonData.defaultSkin = defaultSkin;
			skeletonData.AddSkin (defaultSkin);
		}
		
		//Skin
		for(int i =0,n=ReadInt();i<n;i++)
			skeletonData.AddSkin (readSkin (ReadString (),m_nonessential));
		
		//Events
		for(int i =0,n=ReadInt();i<n;i++)
		{
			EventData eventData = new EventData(ReadString ());
			eventData.Int = ReadInt (false);
			eventData.Float= ReadFloat ();
			eventData.String = ReadString ();
			skeletonData.AddEvent(eventData);
		}
		
		//Animations
		for(int i=0,n=ReadInt ();i<n;i++)
		{
			readAnimation(ReadString(),skeletonData);
		}
		
		skeletonData.bones.TrimExcess();
		skeletonData.slots.TrimExcess();
		skeletonData.skins.TrimExcess();
		skeletonData.animations.TrimExcess();
		return skeletonData;
	}	
	
	void readAnimation(string name,SkeletonData skeletonData)
	{
		List<Timeline> timelines = new List<Timeline>();
		float scale = 1;
		float duration = 0;
		
		//Slot timelines
		for(int i = 0,n = ReadInt ();i<n;i++)
		{
			int slotIndex = ReadInt ();
			for(int ii=0,nn = ReadInt ();ii<nn;ii++)
			{
				int timelineType = Read ();
				int frameCount = ReadInt ();
				switch(timelineType)
				{
				case TIMELINE_COLOR:
				{
					ColorTimeline timeline = new ColorTimeline(frameCount);
					timeline.slotIndex = slotIndex;
					for(int frameIndex = 0;frameIndex < frameCount;frameIndex++)
					{
						float time = ReadFloat ();
						m_tempColor = ReadColor (); //Color
						
						timeline.setFrame (frameIndex,time,m_tempColor.r,m_tempColor.g,m_tempColor.b,m_tempColor.a);
						if(frameIndex < frameCount -1) readCurve(frameIndex,timeline);
						
					}
					timelines.Add (timeline);
					duration = Mathf.Max(duration,timeline.Frames[frameCount*5 - 5]);
					break;
				}	
				case TIMELINE_ATTACHMENT:
				{
					AttachmentTimeline timeline = new AttachmentTimeline(frameCount);
					timeline.slotIndex = slotIndex;
					for(int frameIndex = 0;frameIndex <frameCount;frameIndex++)
						timeline.setFrame (frameIndex,ReadFloat (),ReadString ());
					timelines.Add (timeline);
					duration = Mathf.Max (duration,timeline.Frames[frameCount -1]);
					break;
				}
				}
			}
		}
		
		//Bone timelines
		for(int i = 0,n = ReadInt ();i<n;i++)
		{
			int boneIndex = ReadInt();
			for (int ii = 0, nn = ReadInt(); ii < nn; ii++) {
				int timelineType = Read();
				int frameCount = ReadInt();
				switch (timelineType) {
				case TIMELINE_ROTATE: {
					RotateTimeline timeline = new RotateTimeline(frameCount);
					timeline.boneIndex = boneIndex;
					for (int frameIndex = 0; frameIndex < frameCount; frameIndex++) {
						timeline.SetFrame(frameIndex, ReadFloat(), ReadFloat());
						if (frameIndex < frameCount - 1) readCurve(frameIndex, timeline);
					}
					timelines.Add(timeline);
					duration = Mathf.Max(duration, timeline.Frames[frameCount * 2 - 2]);
					break;
				}
				case TIMELINE_TRANSLATE:
				case TIMELINE_SCALE:
				{
					TranslateTimeline timeline;
					float timelineScale = 1;
					if (timelineType == TIMELINE_SCALE)
						timeline = new ScaleTimeline(frameCount);
					else {
						timeline = new TranslateTimeline(frameCount);
						timelineScale = scale;
					}
					timeline.boneIndex = boneIndex;
					for (int frameIndex = 0; frameIndex < frameCount; frameIndex++) {
						timeline.SetFrame(frameIndex, ReadFloat(), ReadFloat() * timelineScale, ReadFloat()
						                  * timelineScale);
						if (frameIndex < frameCount - 1) readCurve( frameIndex, timeline);
					}
					timelines.Add(timeline);
					duration = Mathf.Max(duration, timeline.Frames[frameCount * 3 - 3]);
					break;
				}
				}
			}
			
		}
		
		//FFD timelines
		for (int i = 0, n = ReadInt(); i < n; i++) {
			Skin skin = skeletonData.skins[ReadInt ()+1];
			for (int ii = 0, nn = ReadInt(); ii < nn; ii++)
			{
				int slotIndex = ReadInt();
				for (int iii = 0, nnn = ReadInt(); iii < nnn; iii++)
				{
					Attachment attachment = skin.GetAttachment(slotIndex, ReadString());
					int frameCount = ReadInt();
					FFDTimeline timeline = new FFDTimeline(frameCount);
					timeline.slotIndex = slotIndex;
					timeline.attachment = attachment;
					for (int frameIndex = 0; frameIndex < frameCount; frameIndex++)
					{
						float time = ReadFloat();
						
						float[] vertices;
						int vertexCount;
						if (attachment.GetType() == typeof(MeshAttachment))
							vertexCount = ((MeshAttachment)attachment).vertices.Length;
						else
							vertexCount = ((SkinnedMeshAttachment)attachment).weights.Length / 3 * 2;
						
						int end = ReadInt();
						if (end == 0) {
							if (attachment.GetType() == typeof(MeshAttachment))
								vertices = ((MeshAttachment)attachment).vertices;
							else
								vertices = new float[vertexCount];
						} else {
							vertices = new float[vertexCount];
							int start = ReadInt();
							end += start;
							if (scale == 1) {
								for (int v = start; v < end; v++)
									vertices[v] = ReadFloat();
							} else {
								for (int v = start; v < end; v++)
									vertices[v] = ReadFloat() * scale;
							}
							if (attachment.GetType () == typeof(MeshAttachment)) {
								float[] meshVertices = ((MeshAttachment)attachment).vertices;
								for (int v = 0, vn = vertices.Length; v < vn; v++)
									vertices[v] += meshVertices[v];
							}
						}
						timeline.setFrame(frameIndex, time, vertices);
						if (frameIndex < frameCount - 1) readCurve(frameIndex, timeline);
					}
					timelines.Add(timeline);
					duration = Mathf.Max(duration, timeline.Frames[frameCount - 1]);
				}
			}
		}
		
		//Draw order timeline
		int drawOrderCount = ReadInt();
		if (drawOrderCount > 0) {
			DrawOrderTimeline timeline = new DrawOrderTimeline(drawOrderCount);
			int slotCount = skeletonData.slots.Count;
			for (int i = 0; i < drawOrderCount; i++) {
				int offsetCount = ReadInt();
				int[] drawOrder = new int[slotCount];
				for (int ii = slotCount - 1; ii >= 0; ii--)
					drawOrder[ii] = -1;
				int[] unchanged = new int[slotCount - offsetCount];
				int originalIndex = 0, unchangedIndex = 0;
				for (int ii = 0; ii < offsetCount; ii++) {
					int slotIndex = ReadInt();
					// Collect unchanged items.
					while (originalIndex != slotIndex)
						unchanged[unchangedIndex++] = originalIndex++;
					// Set changed items.
					drawOrder[originalIndex + ReadInt()] = originalIndex++;
				}
				// Collect remaining unchanged items.
				while (originalIndex < slotCount)
					unchanged[unchangedIndex++] = originalIndex++;
				// Fill in unchanged items.
				for (int ii = slotCount - 1; ii >= 0; ii--)
					if (drawOrder[ii] == -1) drawOrder[ii] = unchanged[--unchangedIndex];
				timeline.setFrame(i, ReadFloat(), drawOrder);
			}
			timelines.Add(timeline);
			duration = Mathf.Max(duration, timeline.Frames[drawOrderCount - 1]);
		}
		
		//Event timeline
		int eventCount = ReadInt();
		if (eventCount > 0) {
			EventTimeline timeline = new EventTimeline(eventCount);
			for (int i = 0; i < eventCount; i++) {
				float time = ReadFloat();
				EventData eventData = skeletonData.events[ReadInt()];
				Spine.Event event1 = new Spine.Event(eventData);
				event1.Int= ReadInt(false);
				event1.Float= ReadFloat();
				event1.String = ReadBoolean() ? ReadString() : eventData.String;
				timeline.setFrame(i, time, event1);
			}
			timelines.Add (timeline);
			duration = Mathf.Max(duration, timeline.Frames[eventCount - 1]);
		}
		
		timelines.TrimExcess();
		skeletonData.AddAnimation(new Spine.Animation(name,timelines,duration));
	}
	
	
	Skin readSkin(string skinName,bool nonessential)
	{
		int slotCount = ReadInt ();
		if(slotCount==0) return null;
		Skin skin = new Skin(skinName);
		
		for(int i=0;i<slotCount;i++)
		{
			int slotIndex = ReadInt ();
			int jCount = ReadInt ();
			for(int j=0;j<jCount;j++)
			{
				string name = ReadString ();
				skin.AddAttachment(slotIndex,name,readAttachment(skin,name,nonessential));
			}
		}
		return skin;
	}
	
	Attachment readAttachment(Skin skin,string attachmentName,bool nonessential)
	{
		
		string name = ReadString ();
		if(name == "") name = attachmentName;
		
		int type = Read();
		switch((AttachmentType)type)
		{
		case AttachmentType.region:
		{
			string path = ReadString ();
			if(path == "") path = name;
			
			RegionAttachment region = m_attachmentLoader.NewRegionAttachment(skin,name,path);
			if(region == null) return null;
			region.Path = path;
			region.X = ReadFloat ();
			region.Y = ReadFloat ();
			region.ScaleX = ReadFloat ();
			region.ScaleY = ReadFloat ();
			region.Rotation = ReadFloat ();
			region.Width = ReadFloat ();
			region.Height = ReadFloat ();
			//Color
			Color color = ReadColor ();
			region.R = color.r;
			region.G = color.g;
			region.B = color.b;
			region.A = color.a;
			
			region.UpdateOffset();
			return region;
		}
		case AttachmentType.boundingbox:
		{
			BoundingBoxAttachment box = m_attachmentLoader.NewBoundingBoxAttachment(skin,name);
			if(box == null) return null;
			box.Vertices = readFloatArray();
			return box;
		}
		case AttachmentType.mesh:
		{
			string path = ReadString ();
			if(path == "") path = name;
			MeshAttachment mesh = m_attachmentLoader.NewMeshAttachment(skin,name,path);
			if(mesh == null) return null;
			mesh.Path = path;
			float[] uvs = readFloatArray();
			int[] triangles = readShortArray();
			float[] vertices = readFloatArray();
			mesh.vertices = vertices;
			mesh.Triangles = triangles;
			mesh.regionUVs = uvs;
			mesh.UpdateUVs();
			//Color
			Color color=ReadColor ();
			mesh.R = color.r;
			mesh.G = color.g;
			mesh.B = color.b;
			mesh.A = color.a;
			
			mesh.HullLength = ReadInt()*2;
			if(nonessential)
			{
				mesh.Edges = readIntArray();
				mesh.Width = ReadFloat();
				mesh.Height= ReadFloat ();
			}
			
			return mesh;
		}
		case AttachmentType.skinnedmesh:
		{
			string path = ReadString ();
			if(path == "") path = name;
			SkinnedMeshAttachment mesh = m_attachmentLoader.NewSkinnedMeshAttachment(skin,name,path);
			if(mesh == null) return null;
			mesh.Path = path;
			float[] uvs = readFloatArray();
			int[] triangles = readShortArray ();
			
			int vertexCount = ReadInt ();
			List<float> weights = new List<float>(uvs.Length*3*3);
			List <int> bones = new List<int>(uvs.Length*3);
			
			for(int i =0;i<vertexCount;i++)
			{
				int boneCount =(int)ReadFloat();
				bones.Add (boneCount);
				for(int j=i+boneCount*4;i<j;i+=4)
				{
					bones.Add ((int)ReadFloat ());
					weights.Add (ReadFloat ());
					weights.Add (ReadFloat ());
					weights.Add (ReadFloat ());
				}
				
			}
			mesh.bones = bones.ToArray();
			mesh.Weights = weights.ToArray ();
			mesh.Triangles = triangles;
			mesh.regionUVs = uvs;
			mesh.UpdateUVs();
			//Color
			Color color=ReadColor ();
			mesh.R = color.r;
			mesh.G = color.g;
			mesh.B = color.b;
			mesh.A = color.a;
			
			mesh.HullLength = ReadInt()*2;
			if(nonessential)
			{
				mesh.Edges = readIntArray();
				mesh.Width = ReadFloat();
				mesh.Height = ReadFloat();
			}
			return mesh;
		}
		}
		return null;
	}
	
	bool ReadBoolean()
	{
		byte []bytebool = new byte [1];
		System.Array.Copy(m_temp,m_index,bytebool,0,bytebool.Length);
		m_index +=bytebool.Length;
		bool boolean = System.BitConverter.ToBoolean(bytebool,0);
		return boolean;
	}
	
	int Read()
	{
		int a = m_temp[m_index];
		m_index+=1;
		return a;
	}
	
	int ReadInt(bool optimizePositive=true)
	{
		int b = Read ();
		int result = b&0x7F;
		if((b & 0x80)!=0)
		{
			b = Read();
			result |= (b&0x7F)<<7;
			if((b&0x80)!=0)
			{
				b = Read();
				result |= (b&0x7F)<<14;
				if((b&0x80)!=0)
				{
					b = Read ();
					result |= (b&0x7F)<<21;
					if((b&0x80)!=0)
					{
						b=Read ();
						result |=(b&0x7F)<<28;
					}
				}
			}
			
		}
		return optimizePositive ? result : ((result >>1) ^ -(result & 1));
	}
	
	string ReadString()
	{
		int length = ReadInt ();
		if(length>0)
			length -=1;
		byte[] byteStr = new byte[length];
		System.Array.Copy (m_temp,m_index,byteStr,0,byteStr.Length);
		m_index += byteStr.Length;
		string str= System.Text.Encoding.Default.GetString (byteStr);
		return str;
	}
	
	float ReadFloat()
	{
		int a = Read();
		int b = Read();
		int c = Read();
		int d = Read();
		int e =(a << 24) | (b << 16) | (c << 8) | d;
		byte[] byteTemp=System.BitConverter.GetBytes(e);
		float fTemp= System.BitConverter.ToSingle (byteTemp,0);
		return fTemp;
	}
	
	int ReadShort()
	{
		int a = Read();
		int b = Read();
		int c = (a<<8)|b;
		return c;
	}
	
	Color ReadColor()
	{
		float r = (Read()&0x000000ff)/(float)255;
		float g = (Read()&0x000000ff)/(float)255;
		float b = (Read()&0x000000ff)/(float)255;
		float a = (Read()&0x000000ff)/(float)255;
		return new Color(r,g,b,a);
	}
	
	
	float[] readFloatArray()
	{
		int n = ReadInt ();
		float [] array = new float[n];
		for(int i =0;i<n;i++)
			array[i] = ReadFloat ();
		return array;
	}
	
	int[] readShortArray()
	{
		int n = ReadInt ();
		int[] array = new int[n];
		for(int i =0;i<n;i++)
			array[i] = ReadShort ();
		return array;
	}
	
	int[] readIntArray()
	{
		int n = ReadInt();
		int[] array = new int[n];
		for (int i = 0; i < n; i++)
			array[i] = ReadInt();
		return array;
	}
	
	void readCurve(int frameIndex,CurveTimeline timeline)
	{
		switch(Read ())
		{
		case CURVE_STEPPED:
			timeline.SetStepped(frameIndex);
			break;
		case CURVE_BEZIER:
			setCurve(timeline,frameIndex,ReadFloat (),ReadFloat (),ReadFloat (),ReadFloat ());
			break;
		}
	}
	
	void setCurve(CurveTimeline timeline,int frameIndex,float cx1,float cy1,float cx2,float cy2)
	{
		timeline.SetCurve (frameIndex,cx1,cy1,cx2,cy2);
	}
}
