//作者:张伟刚
//时间：2015/05/22
//功能描述：小兵基础数据结构
//备注：
using UnityEngine;
using System.Collections;

public class PlayerSoldier
{
    public int id{set;get;}//兵的ID
    public int baseID{set;get;}//兵的基础ID
    public int maxLv{set;get;}//等级上限
    public int lv{set;get;}//当前等级
    public int star{set;get;}//星级
    public int curExp{set;get;}//当前经验
    public int evolve{set;get;}//突破次数
    public int atk{set;get;}//攻击
    public int hp{set;get;}//血量
    public int food{set;get;}//生产消耗
    public int openS{set;get;}//技能是否开放
    public int mark{set;get;}//是否上阵
    public int locked{set;get;}//是否加锁

    private SoldierBase baseData = null;

    public PlayerSoldier()
    {

    }

    public PlayerSoldier(int id,int roleId)
    {
        this.id = id;
        this.lv = 1;
        this.curExp = 0;
        this.evolve = 0;
        this.openS = ConstData.FALSE;
        this.mark = ConstData.FALSE;
        this.locked = ConstData.FALSE;
        baseData = MyApp.GetInstance().GetDataManager().GetSoldierData(roleId);
        if(baseData != null)
        {
            this.baseID = baseData.BaseId;
            this.maxLv = baseData.MaxLv;
            this.star = baseData.Star;
            this.atk = (int)baseData.Atk;
            this.hp = (int)baseData.HP;
            this.food = baseData.Food;
        }
    }

    public PlayerSoldier(PlayerSoldierData data)
    {
        this.id = data.Id;
        this.baseID = data.BaseId;
        this.maxLv = data.MaxLv;
        this.lv = data.Lv;
        this.star = data.Star;
        this.curExp = data.CurExp;
        this.evolve = data.Evolve;
        this.atk = (int)data.Atk;
        this.hp = (int)data.HP;
        this.food = data.Food;
        this.openS = data.OpenS;
        this.mark = data.Mark;
        this.locked = data.Locked;
    }

    public SoldierBase GetBaseData()
    {
        if(baseData == null)
        {
            baseData = MyApp.GetInstance().GetDataManager().GetSoldierData(this.baseID, this.star);
        }
        return baseData;
    }

    public void SetBaseData(SoldierBase data)
    {
        this.baseData = data;
    }

    public void ResetValue(PlayerSoldier pSoldier)
    {
        this.id = pSoldier.id;
        this.baseID = pSoldier.baseID;
        this.maxLv = pSoldier.maxLv;
        this.lv = pSoldier.lv;
        this.star = pSoldier.star;
        this.curExp = pSoldier.curExp;
        this.evolve = pSoldier.evolve;
        this.atk = pSoldier.atk;
        this.hp = pSoldier.hp;
        this.food = pSoldier.food;
        this.openS = pSoldier.openS;
        this.mark = pSoldier.mark;
        this.locked = pSoldier.locked;
        this.baseData = pSoldier.baseData;
    }

    //Clone函数用于实例化
    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
