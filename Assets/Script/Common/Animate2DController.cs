using UnityEngine;
using Spine;
using System;

public class Animate2DController{
    const string Path_Hit = "FightScene/HitEffect/Hit";
    const string Path_Effect = "FightScene/SkillEffect/Effect";
    const string Path_Buff = "FightScene/BuffEffect/Buff";
    const string Path_Cast = "FightScene/CastObject/Cast";
    const string Path_UISkill = "FightScene/UISkill/UISkill";


    public static void InitSoldier(string pathName)
    {
        Resources.Load<SkeletonDataAsset>("FightScene/Monster/" + pathName + "/New SkeletonData");
        Resources.Load<AtlasAsset>("FightScene/Monster/" + pathName + "/New Atlas");
        Resources.Load<Material>("FightScene/Monster/" + pathName + "/New Material");
    }


    public static bool SetSoldier(GameObject go,int id,bool isLeft)
    {
        if (go.GetComponent<SkeletonAnimation>() == null)
            return false;

        //Debug.Log("SetSoldier id = " + id);
        string skel;
        if (isLeft)
        {
            //Debug.Log("isLeft SetSoldier id = " + id);
			//PlayerSoldier soldier = PlayerDataManager.GetInstance().GetSoldier(id);
            //skel = soldier.GetBaseData().Skl;
			//Debug.Log("skel = " + skel);
           
        }
        else
        {
           // Debug.Log("not isLeft SetSoldier id = " + id);
            if (MyApp.GetInstance().GetDataManager() == null)
            {
                Debug.Log("not isLeft SetSoldier id = " + id + "null; = " + MyApp.GetInstance());
            }
            //skel = MyApp.GetInstance().GetDataManager().GetMonsterData(id).Skl;
            //Debug.Log("skel = " + skel);
        }
        //SkeletonDataAsset skeleton = GameObject.Instantiate(Resources.Load<SkeletonDataAsset>("FightScene/" + "Skl/"+ skel + "/New SkeletonData")) as SkeletonDataAsset;
        ////Debug.Log("skeleton = " + skeleton + "----------------FightScene/" + "Skl/" + skel + "/New SkeletonData");
        //AtlasAsset assetData = GameObject.Instantiate(Resources.Load<AtlasAsset>("FightScene/" + "Skl/" + skel + "/New Atlas")) as AtlasAsset;
        //Material material = GameObject.Instantiate(Resources.Load<Material>("FightScene/" + "Skl/" + skel + "/New Material")) as Material;
        //if (!skeleton || !assetData || !material)
        //    return false;
        //skeleton.atlasAsset = assetData;
        //assetData.materials[0] = material;
        //go.GetComponent<SkeletonAnimation>().skeletonDataAsset = skeleton;
        //go.GetComponent<SkeletonAnimation>().Reset();
       
        
        return true;
    }

    public static bool SetSoldier(GameObject go,string pathName,int solidify)
    {
        Debug.Log("SetSoldier GameObject go,string pathName,int solidify-----------------");
        if (go.GetComponent<SkeletonAnimation>() == null)
            return false;
        string name;


        if (solidify < 5)
        {
            name = pathName + (solidify + 1).ToString();
        } 
        else
        {
            name = pathName + "JY";
        }
        
        Debug.Log(name);
        SkeletonDataAsset skeleton = GameObject.Instantiate(Resources.Load<SkeletonDataAsset>("FightScene/Monster/" + name + "/New SkeletonData")) as SkeletonDataAsset;
        AtlasAsset assetData = GameObject.Instantiate(Resources.Load<AtlasAsset>("FightScene/Monster/" + name + "/New Atlas")) as AtlasAsset;
        Material material = GameObject.Instantiate(Resources.Load<Material>("FightScene/Monster/" + name + "/New Material")) as Material;
        if (!skeleton || !assetData || !material)
            return false;
        skeleton.atlasAsset = assetData;
        assetData.materials[0] = material;
        go.GetComponent<SkeletonAnimation>().skeletonDataAsset = skeleton;
        go.GetComponent<SkeletonAnimation>().Reset();
        
        return true;
    }

    public static bool SetHit(GameObject go,string pathName)
    {
        return SetAnimator(Path_Hit, pathName, go);
    }

    public static bool SetEffect(GameObject go,string pathName)
    {
        return SetAnimator(Path_Effect, pathName, go);
    }

    public static bool SetBuff(GameObject go,string pathName)
    {
        return SetAnimator(Path_Buff,pathName,go);
    }

    public static bool SetCast(GameObject go,string pathName)
    {
        return SetAnimator(Path_Cast,pathName,go);
    }

    public static bool SetUISkillEffect(GameObject go,string pathName)
    {
        return SetAnimator(Path_UISkill,pathName,go);
    }

    public static bool SetAnimator(string path,string pathname,GameObject go)
    {
		//Debug.Log("SetAnimator(string path,string pathname,GameObject go) path + pathname + /New SkeletonData" + path + pathname + "/New SkeletonData");
        if (go.GetComponent<SkeletonAnimation>() == null)
            return false;
            
        //Debug.Log(path + pathname);
        Debug.Log("path + pathname + /New SkeletonData = " + (path + pathname + "/New SkeletonData"));
        SkeletonDataAsset skeleton = Resources.Load<SkeletonDataAsset>(path + pathname + "/New SkeletonData");
        AtlasAsset assetData;
        Material material;
        if(skeleton == null)
        {
            skeleton = GameObject.Instantiate(Resources.Load<SkeletonDataAsset>("FightScene/CastObject/Cast10004/New SkeletonData")) as SkeletonDataAsset;
            assetData = GameObject.Instantiate(Resources.Load<AtlasAsset>("FightScene/CastObject/Cast10004/New Atlas")) as AtlasAsset;
            material = GameObject.Instantiate(Resources.Load<Material>("FightScene/CastObject/Cast10004/New Material")) as Material;
        }
        else
        {
            skeleton = GameObject.Instantiate(Resources.Load<SkeletonDataAsset>(path + pathname + "/New SkeletonData")) as SkeletonDataAsset;
            assetData = GameObject.Instantiate(Resources.Load<AtlasAsset>(path + pathname + "/New Atlas")) as AtlasAsset;
            material = GameObject.Instantiate(Resources.Load<Material>(path + pathname + "/New Material")) as Material;
        }
        if (!skeleton || !assetData || !material)
            return false;
        skeleton.atlasAsset = assetData;
        assetData.materials[0] = material;
        go.GetComponent<SkeletonAnimation>().skeletonDataAsset = skeleton;
        go.GetComponent<SkeletonAnimation>().Reset();
        return true;
    }


    public static Vector3 GetHeadEffectPos(GameObject obj)
    {
        Bone bone =obj.GetComponent<SkeletonAnimation>().skeleton.FindBone("HeadEffect");
        Vector3 posOffset  = new Vector3(bone.worldX,bone.WorldY,0);
        return posOffset;
    }

    public static Vector3 GetBodyEffectPos(GameObject obj)
    {
        Bone bone =obj.GetComponent<SkeletonAnimation>().skeleton.FindBone("BodyEffect");
        Vector3 posOffset  = new Vector3(bone.worldX,bone.WorldY,0);
        return posOffset;
    }    

    public static Vector3 GetFootEffectPos(GameObject obj)
    {
        Bone bone =obj.GetComponent<SkeletonAnimation>().skeleton.FindBone("FootEffect");
        Vector3 posOffset  = new Vector3(bone.worldX,bone.WorldY,0);
        return posOffset;
    }

    public static Vector3 GetHitEffectPos(GameObject obj)
    {
        Bone bone = obj.GetComponent<SkeletonAnimation>().skeleton.FindBone("HitPos");
        Vector3 posOffset = new Vector3(bone.worldX,bone.worldY,0);
        return posOffset;
    }

    public static bool SetUIEffect(GameObject go,string pathName)
    {
        Debug.Log("SetUIEffect(GameObject go,string pathName)");
        if (go.GetComponent<SkeletonAnimation>() == null)
            return false;
            
        SkeletonDataAsset skeleton = GameObject.Instantiate(Resources.Load<SkeletonDataAsset>(pathName+ "/New SkeletonData")) as SkeletonDataAsset;
        if (!skeleton )
            return false;
        go.GetComponent<SkeletonAnimation>().skeletonDataAsset = skeleton;
        go.GetComponent<SkeletonAnimation>().Reset();
        return true;
    }
}
