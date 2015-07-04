using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// 所有场景中都用到的不销毁的游戏对象
/// </summary>
public class PublicGameObject : MonoBehaviour
{
		public static GameObject publicGameObj;

		void Awake()
		{
            DontDestroyOnLoad(gameObject);
			publicGameObj = gameObject;
		}

		void Start ()
		{
#if UNITY_ANDROID
            CheckSelf();
#endif
				//InvokeRepeating ("reduceTime", 0.1f, 1f);
		}

		void Update ()
		{
		}

		void reduceTime ()
		{
				//PlayerDataManager.GetInstance ().renderReduceTime ();
		}
        
        void CheckSign()
        {
#if UNITY_ANDROID
            //检测数据签名
            AndroidJavaClass unity = new AndroidJavaClass("com.untiy3d.player.UnitPlayer");
            AndroidJavaObject activity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject manager = activity.Call<AndroidJavaObject>("getPackageManager");
            string name = activity.Call<string>("getPackageName");

            var GET_SIGNATURES = 64;
            AndroidJavaObject packageInfo = manager.Call<AndroidJavaObject>("getPackageInfo", name, GET_SIGNATURES);
            AndroidJavaObject[] signatures = packageInfo.Get<AndroidJavaObject[]>("signatures");

            for (int i = 0; i < signatures.Length; i++)
            {
                Debug.Log("signature" + signatures[i].Call<string>("toChasString"));
                Debug.Log("signature hash = " + signatures[i].Call<int>("hashCode").ToString("X"));
            }
#endif
        }

        IEnumerable CheckDex()
        {
            //检测classes.dex是否被更改
            string urlScheme = "jar:file://";
            string apkPath = Application.dataPath;
            string separator = "!/";
            string entry = "classes.dex";
            string url = urlScheme + apkPath + separator + entry;

            //Read classes.dex inside package.apk
            WWW www = new WWW(url);
            yield return www;

            //Calculate the MD5 sum of classes.dex contents
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(www.bytes);

            //print MD5 sum
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            Debug.Log("md5sum(class.dex) = " + sb.ToString());
        }

        void CheckNative()
        {
#if UNITY_ANDROID
            //Native libs check(UnityScript)
            //Retrieve main Activity
            var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var activity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            //Retrieve ApplicationInfo and nativeLibraryDir(N.B. API-9 or newer only!)
            var info = activity.Call<AndroidJavaObject>("getApplicationInfo");
            var nativeLibraryDir = info.Get<string>("nativeLibraryDir");
            var unityPath = Path.Combine(nativeLibraryDir, "libunity.so");

            var file = new FileStream(unityPath, FileMode.Open, FileAccess.Read);
            var sha1 = new SHA1CryptoServiceProvider();
            var hash = sha1.ComputeHash(file);
            file.Close();

            //Print SHA1sum
            var sb = new System.Text.StringBuilder();
            for(int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            Debug.Log("sha1sum(libunity.so) = " + sb.ToString());
#endif
        }

        void CheckSelf()
        {
            //CheckSign();
            //CheckDex();
            //CheckNative();
        }
        
}
