using UnityEngine;
using UnityEngine.UI;

using System.Collections;

using catwins.flexui;

public class Template : MonoBehaviour {

	public GameObject Container;
	
	public UnityEngine.Object XML;

	public InputField Title;
	public InputField Message;
	
	public Button BtnShow;
	
	
	void Start () {
		BtnShow.onClick.AddListener (Show);
		Show ();
	}
	
	Document doc;
	private void Show()
	{
		if (XML == null) {
			Debug.LogError("XML not set");
			return;
		}
		
		if (doc != null) {
			doc.Dispose();
			doc = null;
		}

		string templateStr = XML.ToString ();
		DialogDescriptor desc = new DialogDescriptor ();
		desc.Title = Title.text;
		desc.Content = Message.text;

		DotLiquid.Template template = DotLiquid.Template.Parse(templateStr);
		string content = template.Render (desc.ToHash ());

		doc = new Document (Container, content);

	}


}

public class DialogDescriptor
{

	public string Title = "Title";
	public string Content = "Message";

	public bool ShowOK = true;
	public string OKButtonName = "OK";

	public bool ShowCancel = false;
	public string CancelButtonName = "Cancel";

	public bool ShowCustom = false;
	public string CustomButtonName = "";


	public DotLiquid.Hash ToHash()
	{
		DotLiquid.Hash hash = new DotLiquid.Hash();

		hash ["Title"] = Title;
		hash ["Content"] = Content;

		hash ["ShowOK"] = ShowOK;
		hash ["OKButtonName"] = OKButtonName;

		hash ["ShowCancel"] = ShowCancel;
		hash ["CancelButtonName"] = CancelButtonName;

		hash ["ShowCustom"] = ShowCustom;
		hash ["CustomButtonName"] = CustomButtonName;

		return hash;
	}

}
