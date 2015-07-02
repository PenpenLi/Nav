using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading;


//主线程中尤其是UI部分很多实例都是不允许子线程修改的，
//那么我们就只有想办法把这些子线程里的数据缓存起来，让主线程自己拿着这些数据该干嘛干嘛去
public class EventDispatcher
{
		private static EventDispatcher sinstance;
		public static bool isEnuming = false;
		public delegate void EventCallback (EventBase eb);

		private Dictionary<string, List<EventCallback>> registedCallbacks = new Dictionary<string, List<EventCallback>> ();
		private Dictionary<string, List<EventCallback>> registedCallbacksPending = new Dictionary<string, List<EventCallback>> ();
		private List<EventBase> lPendingEvents = new List<EventBase> ();

		public static EventDispatcher Instance ()
		{
				if (sinstance == null) {
						sinstance = new EventDispatcher ();
				}
				return sinstance;
		}
	
	

		public void RegistEventListener (string eventName, EventCallback eventCallback)
		{
				lock (this) {
						if (!registedCallbacks.ContainsKey (eventName)) {
								registedCallbacks.Add (eventName, new List<EventCallback> ());
						}
			
						if (isEnuming) {
								if (!registedCallbacksPending.ContainsKey (eventName)) {
										registedCallbacksPending.Add (eventName, new List<EventCallback> ());
								}
								registedCallbacksPending [eventName].Add (eventCallback);
								return;
						}
			
						registedCallbacks [eventName].Add (eventCallback);

				}
		}

		//请求超时或断线后，清空所有注册回调
		public void clearnAllRegistedCallBack(){
				Debug.Log ("===============clearnAllRegistedCallBack==============");
				registedCallbacks.Clear ();
		}

		public void UnregistEventListener (string eventName, EventCallback eventCallback)
		{
				lock (this) {
						if (!registedCallbacks.ContainsKey (eventName)) {
								return;
						}
			
						if (isEnuming) {
								Debug.Log ("Cannot unregist event this moment!");
								return;
						}
			
						registedCallbacks [eventName].Remove (eventCallback);
				}
		}
	
		List<EventBase> lEvents = new List<EventBase> ();

		public void DispatchEvent<T> (T eventInstance)
		where T:EventBase
		{
				lock (this) {
						if (!registedCallbacks.ContainsKey (eventInstance.eventName)) {
								return;
						}

						if (isEnuming) {
								lPendingEvents.Add (eventInstance);
								Debug.Log ("Cannot dispatch event this moment!");
								return;
						}
			
						foreach (EventBase eb in lPendingEvents) {
								lEvents.Add (eb);
						}
						lPendingEvents.Clear ();
			
						lEvents.Add (eventInstance);
				}
		}

		public void DispatchEvent (string eventName, object eventValue)
		{
				lock (this) {
						if (!registedCallbacks.ContainsKey (eventName)) {
								Debug.Log("========this call back is not container eventName" + eventName);
								return;
						}
			
						if (isEnuming) {
								lPendingEvents.Add (new EventBase (eventName, eventValue));
								Debug.Log ("Cannot dispatch event this moment!");
								return;
						}
			
						lEvents.Add (new EventBase (eventName, eventValue));
				}
		}
	
		//检测是否有新的事件回调
		public void OnTick ()
		{
				lock (this) {


						foreach (EventBase eb in lPendingEvents) {
								lEvents.Add (eb);
						}
						lPendingEvents.Clear ();
						if (lEvents.Count == 0) {
								foreach (string eventName in registedCallbacksPending.Keys) {
										foreach (EventCallback ec in registedCallbacksPending[eventName]) {
												RegistEventListener (eventName, ec);
										}
								}
								registedCallbacksPending.Clear ();
								return;
						}
						
						isEnuming = true;
						foreach (EventBase eb in lEvents) {
								if (!registedCallbacks.ContainsKey (eb.eventName)) {
										continue;
								}
								for (int i = registedCallbacks[eb.eventName].Count - 1; i >= 0; i--) {
									EventCallback ecb = registedCallbacks [eb.eventName] [i];
									if (ecb == null) {
										continue;
									}
									registedCallbacks [eb.eventName].RemoveAt(i);
									ecb (eb);
									
								}
						}
						lEvents.Clear ();
				}
				isEnuming = false;
		}
}

public class EventBase
{
		public object eventValue;
		public string eventName;

		public EventBase ()
		{
			eventName = this.GetType ().FullName;
		}

		public EventBase (string eventName, object ev)
		{
			this.eventValue = ev;
			this.eventName = eventName;
		}
}
