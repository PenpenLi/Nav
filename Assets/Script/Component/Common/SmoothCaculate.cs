using System.Collections;

/// <summary>
/// 二次指数平滑计算.
/// </summary>
public class SmoothCaculate
{
	/// <summary>
	/// 一次平滑指数
	/// </summary>
	private float s1 = 0.0f;

	/// <summary>
	/// 二次平滑指数.
	/// </summary>
	private float s2 = 0.0f;

	/// <summary>
	/// 平滑系数.
	/// </summary>
	private float coefficient = 0.0f;

	/// <summary>
	/// 上次的数据.
	/// </summary>
	private float lData = 0.0f;

	/// <summary>
	/// 本次的数据.
	/// </summary>
	private float cData = 0.0f;

	/// <summary>
	/// Initializes a new instance of the <see cref="SmoothCaculate"/> class.
	/// </summary>
	/// <param name="coefficient">平滑系数(0~1).</param>
	public SmoothCaculate(float coefficient)
	{
		SetCoefficient(coefficient);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="SmoothCaculate"/> class.
	/// </summary>
	/// <param name="coefficient">平滑系数(0~1).</param>
	/// <param name="data">初始数据.</param>
	public SmoothCaculate(float coefficient,float data)
	{
		SetCoefficient(coefficient);
		this.s1 = data;
		this.s2 = data;
		this.lData = data;
		this.cData = data;
	}
 
	/// <summary>
	/// 设置最新的数据.
	/// </summary>
	/// <param name="data">Data.</param>
	public void SetNewData(float data)
	{
		lData = cData;
		cData = data;
		s1 = coefficient*lData+(1-coefficient)*s1;
		s2 = coefficient*s1+(1-coefficient)*s2;
	}

	/// <summary>
	/// 获取最新的一次平滑指数预测.
	/// </summary>
	/// <returns>The singel exponential value.</returns>
	public float GetSingelExponentialValue()
	{
		return s1;
	}

	/// <summary>
	/// 获取最新的二次平滑指数预测.
	/// </summary>
	/// <returns>The second exponential value.</returns>
	public float GetSecondExponentialValue()
	{
		return s2;
	}

	//预测超前周期
	/// <summary>
	/// 进行某超前周期的二次平滑指数预测.
	/// </summary>
	/// <returns>超前周期.</returns>
	/// <param name="t">T.</param>
	public float GetSecondExponentialValue(int t)
	{
		if(t<0)
		{
			t = 0;
		}

		float a = 2*s1-s2;
		float b = coefficient*(s1-s2)/(1-coefficient);
		return a+b*t;
	}

	/// <summary>
	/// 设置平滑系数.
	/// </summary>
	/// <param name="coefficient">平滑系数.</param>
	private void SetCoefficient(float coefficient)
	{
		if(coefficient<0.0f)
		{
			this.coefficient = 0.0f;
		}else if(coefficient >1.0f)
		{
			this.coefficient = 1.0f;
		}else
		{
			this.coefficient = coefficient;
		}
	}
}
