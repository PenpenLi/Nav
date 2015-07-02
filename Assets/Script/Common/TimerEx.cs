using UnityEngine;
using System.Collections;

public class TimerEx {
	private float m_time = 0;
	private float m_repeatRate = 0;
	private float m_firstRunTime = 0;

	public delegate void VoidDelegate ();
	public VoidDelegate CallBackMethod;

	private bool m_begin= false;

    enum RunType
    {
        Once,
        Repeat
    }
    private RunType m_runType;

	public void Repeat( VoidDelegate method,float time,float repeatRate)
	{
        m_runType = RunType.Repeat;
		CallBackMethod = method;
		m_repeatRate = repeatRate;
		m_firstRunTime = time;
		m_begin = true;
	}

	public void Stop()
	{
		m_begin = false;
		m_time = 0;
		CallBackMethod = null;
	}

    public void Run(VoidDelegate method,float time)
    {
        m_runType = RunType.Once;
        CallBackMethod = method;
        m_firstRunTime = time;
        m_begin = true;
    }

	public void Update()
	{

		if(m_begin)
		{
            if (m_time > m_firstRunTime)
            {
                if (m_runType == RunType.Once)
                {
                    CallBackMethod();
                    Stop();
                    return;
                } else
                {
                    if (m_time > m_repeatRate + m_firstRunTime)
                    {
                        CallBackMethod();
                        m_time -= m_repeatRate;
                    }
                }
            }
            m_time += Time.deltaTime;
		}
	}


}
