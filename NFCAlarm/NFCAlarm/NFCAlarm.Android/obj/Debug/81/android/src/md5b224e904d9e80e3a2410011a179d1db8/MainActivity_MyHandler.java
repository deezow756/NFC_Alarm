package md5b224e904d9e80e3a2410011a179d1db8;


public class MainActivity_MyHandler
	extends android.os.Handler
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_handleMessage:(Landroid/os/Message;)V:GetHandleMessage_Landroid_os_Message_Handler\n" +
			"";
		mono.android.Runtime.register ("NFCAlarm.Droid.MainActivity+MyHandler, NFCAlarm.Android", MainActivity_MyHandler.class, __md_methods);
	}


	public MainActivity_MyHandler ()
	{
		super ();
		if (getClass () == MainActivity_MyHandler.class)
			mono.android.TypeManager.Activate ("NFCAlarm.Droid.MainActivity+MyHandler, NFCAlarm.Android", "", this, new java.lang.Object[] {  });
	}


	public MainActivity_MyHandler (android.os.Handler.Callback p0)
	{
		super (p0);
		if (getClass () == MainActivity_MyHandler.class)
			mono.android.TypeManager.Activate ("NFCAlarm.Droid.MainActivity+MyHandler, NFCAlarm.Android", "Android.OS.Handler+ICallback, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public MainActivity_MyHandler (android.os.Looper p0)
	{
		super (p0);
		if (getClass () == MainActivity_MyHandler.class)
			mono.android.TypeManager.Activate ("NFCAlarm.Droid.MainActivity+MyHandler, NFCAlarm.Android", "Android.OS.Looper, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public MainActivity_MyHandler (android.os.Looper p0, android.os.Handler.Callback p1)
	{
		super (p0, p1);
		if (getClass () == MainActivity_MyHandler.class)
			mono.android.TypeManager.Activate ("NFCAlarm.Droid.MainActivity+MyHandler, NFCAlarm.Android", "Android.OS.Looper, Mono.Android:Android.OS.Handler+ICallback, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void handleMessage (android.os.Message p0)
	{
		n_handleMessage (p0);
	}

	private native void n_handleMessage (android.os.Message p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
