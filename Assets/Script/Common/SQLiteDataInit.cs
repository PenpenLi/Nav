using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;

public class SQLiteDataInit : MonoBehaviour
{
	
	public string fromFileName;
	public string toFileName;
	private string fromFilePath;
	private string persistentFilePath;
	
    public static bool finishLoading = false;
	void Awake ()
	{
		InitSQLiteData ();
	}
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void InitSQLiteData ()
	{

		// a product persistant database path.
		persistentFilePath = Application.persistentDataPath + "/" + toFileName;
        Debug.Log(Application.persistentDataPath);
		if (File.Exists (persistentFilePath)) {
			File.Delete(persistentFilePath);
		}
			#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
						fromFilePath = "file://" + Application.streamingAssetsPath + "/" + fromFileName;
						StartCoroutine ("CopyFile");
			#elif UNITY_WEBPLAYER
						fromFilePath = "StreamingAssets/" + fromFileName;
						StartCoroutine ("CopyFile");
			#elif UNITY_IPHONE
						fromFilePath = Application.dataPath + "/Raw/" + fromFileName;
						CopyFile2IPhone ();
			#elif UNITY_ANDROID
						fromFilePath = Application.streamingAssetsPath + "/" + fromFileName;
						StartCoroutine ("CopyFile");
			#elif UNITY_WP8
						//fromFilePath = Application.streamingAssetsPath + "/" + fromFileName;
						//StartCoroutine ("CopyFile2WP8");
		#endif
	}



	void CopyFile2IPhone ()
	{		
            UnityEngine.Debug.Log ("persistentFilePath:"+persistentFilePath);
			using ( FileStream fs = new FileStream(fromFilePath, FileMode.Open, FileAccess.Read, FileShare.Read) ){
				byte[] bytes = new byte[fs.Length];
				fs.Read(bytes,0,(int)fs.Length);
				File.WriteAllBytes (persistentFilePath, bytes);
			}	
            UnityEngine.Debug.Log ("file copy done");
	}
	
	IEnumerator CopyFile ()
	{
        UnityEngine.Debug.Log ("persistentFilePath:"+persistentFilePath);
		WWW www1 = new WWW (fromFilePath);
		yield return www1;
		try{
			File.WriteAllBytes (persistentFilePath, www1.bytes);
		}catch(Exception e)
		{
			Debug.Log ("Error = " + e.ToString());
		}
        www1.Dispose();

        finishLoading = true;
	}

	public static string GetMD5HashFromFile(string filename)
	{
		try{
			FileStream file = new FileStream(filename, FileMode.Open);
			System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] retVal = md5.ComputeHash(file);
			file.Close();

			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < retVal.Length; i++)
			{
				sb.Append(retVal[i].ToString("x2"));
			}

			return sb.ToString();

		}catch(Exception e)
		{
			throw new Exception("GetMD5HashFromFile() fail, error: " + e.Message);
		}
	}

	void CopyFile2WP8(){
		FileStream fs;
		fs = new FileStream(fromFilePath,FileMode.Open);
		BinaryReader br = new BinaryReader(fs);
		int count = (int)br.BaseStream.Length;
		char[] data = br.ReadChars(count);
		fs.Close();
		//FileStream writefs = new FileStream(persistentFilePath,FileMode.OpenOrCreate,FileAccess.ReadWrite);
		BinaryWriter bw = new BinaryWriter(fs);
		for(int i = 0; i < data.Length; i++){
			bw.Write(data[i]);
		}
		bw.Flush();
		bw.Close();
		br.Close();
	}

}
