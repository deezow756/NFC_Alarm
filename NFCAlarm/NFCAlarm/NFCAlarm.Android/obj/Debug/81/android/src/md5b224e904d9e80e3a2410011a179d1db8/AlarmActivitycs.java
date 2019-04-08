package md5b224e904d9e80e3a2410011a179d1db8;


public class AlarmActivitycs
	extends md51558244f76c53b6aeda52c8a337f2c37.FormsAppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("NFCAlarm.Droid.AlarmActivitycs, NFCAlarm.Android", AlarmActivitycs.class, __md_methods);
	}


	public AlarmActivitycs ()
	{
		super ();
		if (getClass () == AlarmActivitycs.class)
			mono.android.TypeManager.Activate ("NFCAlarm.Droid.AlarmActivitycs, NFCAlarm.Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
