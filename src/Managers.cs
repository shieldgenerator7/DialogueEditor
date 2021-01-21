using DialogueEditor;
using DialogueEditor.src;
using System;
using System.Drawing;
using System.IO;

public class Managers
{
    private static Managers instance;

    private readonly frmMain mainForm;

    private readonly NodeManager nodeManager;
    public static NodeManager Node
        => instance.nodeManager;

    private readonly ControlManager controlManager;
    public static ControlManager Control
        => instance.controlManager;

    private readonly SelectionManager selectionManager;
    public static SelectionManager Select
        => instance.selectionManager;

    private readonly FileManager fileManager;
    public static FileManager File
        => instance.fileManager;

    private readonly DisplayManager displayManager;
    public static DisplayManager Display
        => instance.displayManager;

    private readonly LayoutManager layoutManager;
    public static LayoutManager Layout
        => instance.layoutManager;

    private readonly ColorSettings colorSettings;
    public static ColorSettings Colors
        => instance.colorSettings;

    private readonly ImageBank images;
    public static ImageBank Images
        => instance.images;

    public static frmMain Form
        => instance.mainForm;

    public static void init(frmMain mf)
    {
        if (instance == null)
        {
            new Managers(mf);
        }
    }

    public Managers(frmMain mf)
    {
        instance = this;
        this.mainForm = mf;
        this.nodeManager = new NodeManager();
        this.controlManager = new ControlManager();
        this.selectionManager = new SelectionManager();
        this.fileManager = new FileManager();
        this.displayManager = new DisplayManager();
        this.layoutManager = new LayoutManager();
        this.colorSettings = new ColorSettings();
        this.images = new ImageBank();
    }

    public static bool Initialized => instance != null;
}
