using UnityEngine;
using System.Collections;
using NetManager;

namespace NetManager
{
    public class PushMsgReciver
    {
        private static PushMsgReciver instance = null;

        public static PushMsgReciver GetInstance()
        {
            if (instance == null)
            {
                instance = new PushMsgReciver();
            }
            return instance;
        }


        /// <summary>
        /// 注册并开始监听推送消息.
        /// </summary>
        public void StartReciver()
        {
            ConnectionManager manager = ConnectionManager.GetInstance();
//            manager.ReceivePushMsg<int>(CGNetConst.ROUTE_PUSHMSG_MAIL, RecvMailPushMsgCallback);
//            manager.ReceivePushMsg<UnionBrief>(CGNetConst.ROUTE_PUSHMSG_UNION, RecvUnionPushMsgCallback);
        }

        //处理邮件推送消息
        private void RecvMailPushMsgCallback(EventBase eb)
        {
            string eventname = eb.eventName;
            object obj = eb.eventValue;
        
//            if (CGNetConst.ROUTE_PUSHMSG_MAIL.Equals(eventname))
//            {
//                if (obj != null)
//                {
//                    
//                }
//            }
        }
   
    }
}
