namespace Bussen.Resources.Styles;

public partial class Common : ResourceDictionary
{
	public Common()
	{
		InitializeComponent();

#if DEBUG
		this["DebugMode"] = true;
#else
		this["DebugMode"] = false;
#endif
    }
}