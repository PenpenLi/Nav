/******************************************************************************
 * Spine Runtimes Software License
 * Version 2.1
 * 
 * Copyright (c) 2013, Esoteric Software
 * All rights reserved.
 * 
 * You are granted a perpetual, non-exclusive, non-sublicensable and
 * non-transferable license to install, execute and perform the Spine Runtimes
 * Software (the "Software") solely for internal use. Without the written
 * permission of Esoteric Software (typically granted by licensing Spine), you
 * may not (a) modify, translate, adapt or otherwise create derivative works,
 * improvements of the Software or develop new applications using the Software
 * or (b) remove, delete, alter or obscure any trademarks or any copyright,
 * trademark, patent or other intellectual property or proprietary rights
 * notices on or in the Software, including any copy thereof. Redistributionsqq
 * in binary or source form must include this license and terms.
 * 
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE "AS IS" AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL ESOTERIC SOFTARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS;
 * OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
 * OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Spine;

[ExecuteInEditMode]
[AddComponentMenu("Spine/SkeletonAnimation")]
public class SkeletonAnimation : SkeletonRenderer {
	public float timeScale = 1;
	public bool loop;
	public Spine.AnimationState state;

	public delegate void UpdateBonesDelegate(SkeletonAnimation skeleton);
	public UpdateBonesDelegate UpdateBones;

    public bool useUnscaleTime = false;

	[SerializeField]
	private String _animationName;
	public String AnimationName {
		get {
			TrackEntry entry = state.GetCurrent(0);
			return entry == null ? null : entry.Animation.Name;
		}
		set {
			if (_animationName == value) return;
			_animationName = value;
			if (value == null || value.Length == 0)
				state.ClearTrack(0);
			else
			{
                //Debug.Log("value = " + value);
				state.SetAnimation(0, value, loop);
				m_isFinish = false;
			}
		}
	}

	private bool m_isFinish = false;
	public bool IsFinish {get {return m_isFinish;} }

	public override void Reset () {
		base.Reset();
		if (!valid) return;
		state = new Spine.AnimationState(skeletonDataAsset.GetAnimationStateData());
		state.Complete+= new Spine.AnimationState.CompleteDelegate(OnComplete);
		if (_animationName != null && _animationName.Length > 0) {
			m_isFinish = false;
			state.SetAnimation(0, _animationName, loop);
			Update(0);
		}
	}

	public virtual void Update () {
        if (useUnscaleTime)
            Update(Time.unscaledDeltaTime);
        else
		    Update(Time.deltaTime);
	}

	public virtual void Update (float deltaTime) {
		if (!valid) return;

		deltaTime *= timeScale;
		skeleton.Update(deltaTime);
		state.Update(deltaTime);
		state.Apply(skeleton);
		if (UpdateBones != null) UpdateBones(this);
		skeleton.UpdateWorldTransform();
	}

	void OnComplete(Spine.AnimationState state, int trackIndex,int loopCount)
	{
		m_isFinish = true;
	}

	public bool isComplete()
	{
		return m_isFinish;
	}

	public void SetAnim(string name,bool isLoop)
	{
		loop = isLoop;
		AnimationName = name;
	}

    public void AddAnim(string name,bool isLoop)
    {
        state.AddAnimation(0,name,isLoop,0);
    }

    public void AddEventListener(Spine.AnimationState.EventDelegate func)
    {
        state.Event += new Spine.AnimationState.EventDelegate(func);
    }

    public void AddOnCompleteListener(Spine.AnimationState.CompleteDelegate func)
    {
        state.Complete += new Spine.AnimationState.CompleteDelegate(func);
    }

    public float GetAnimationProcess()
    {
        TrackEntry track = state.GetCurrent(0);
        return track.Time / track.EndTime;
    }

    public int GetAnimCount()
    {
        return skeleton.Data.Animations.Count;
    }

    public String [] GetAllAnimationName()
    {
        String[] animations = new String[skeleton.Data.Animations.Count];

        for (int i = 0; i < animations.Length ; i++) {
            String name = skeleton.Data.Animations[i].Name;
            animations[i] = name;
        }

        return animations;
    }

}
