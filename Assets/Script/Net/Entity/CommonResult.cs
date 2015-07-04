using System;

/*
 * 通用消息接收类
 */
public class CommonResult<T>
{
	
	public int code{set;get;}
	public int errcode{ set; get;}
	public T data{set;get;}
	
	
	public CommonResult ()
	{
	}
	
	public CommonResult (int code,int errcode,T data)
	{
		this.code = code;
		this.errcode = errcode;
		this.data = data;
	}
	
}

