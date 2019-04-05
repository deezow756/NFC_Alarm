package md5b224e904d9e80e3a2410011a179d1db8;


public class MainActivity
	extends md51558244f76c53b6aeda52c8a337f2c37.FormsAppCompatActivity
	implements
		mono.android.IGCUserPeer,
		android.nfc.NfcAdapter.OnNdefPushCompleteCallback,
		android.nfc.NfcAdapter.CreateNdefMessageCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onNewIntent:(Landroid/content/Intent;)V:GetOnNewIntent_Landroid_content_Intent_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onPause:()V:GetOnPauseHandler\n" +
			"n_onNdefPushComplete:(Landroid/nfc/NfcEvent;)V:GetOnNdefPushComplete_Landroid_nfc_NfcEvent_Handler:Android.Nfc.NfcAdapter/IOnNdefPushCompleteCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_createNdefMessage:(Landroid/nfc/NfcEvent;)Landroid/nfc/NdefMessage;:GetCreateNdefMessage_Landroid_nfc_NfcEvent_Handler:Android.Nfc.NfcAdapter/ICreateNdefMessageCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("NFCAlarm.Droid.MainActivity, NFCAlarm.Android", MainActivity.class, __md_methods);
	}


	public MainActivity ()
	{
		super ();
		if (getClass () == MainActivity.class)
			mono.android.TypeManager.Activate ("NFCAlarm.Droid.MainActivity, NFCAlarm.Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onNewIntent (android.content.Intent p0)
	{
		n_onNewIntent (p0);
	}

	private native void n_onNewIntent (android.content.Intent p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onPause ()
	{
		n_onPause ();
	}

	private native void n_onPause ();


	public void onNdefPushComplete (android.nfc.NfcEvent p0)
	{
		n_onNdefPushComplete (p0);
	}

	private native void n_onNdefPushComplete (android.nfc.NfcEvent p0);


	public android.nfc.NdefMessage createNdefMessage (android.nfc.NfcEvent p0)
	{
		return n_createNdefMessage (p0);
	}

	private native android.nfc.NdefMessage n_createNdefMessage (android.nfc.NfcEvent p0);

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
