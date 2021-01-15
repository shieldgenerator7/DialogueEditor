using DialogueEditor;
using DialogueEditor.src;
using System;
using System.Drawing;
using System.IO;

public class Managers
{
    private static Managers instance;

    private frmMain mainForm;

    private NodeManager nodeManager;
    public static NodeManager Node
        => instance.nodeManager;

    private ControlManager controlManager;
    public static ControlManager Control
        => instance.controlManager;

    private FileManager fileManager;
    public static FileManager File
        => instance.fileManager;

    private DisplayManager displayManager;
    public static DisplayManager Display
        => instance.displayManager;

    private LayoutManager layoutManager;
    public static LayoutManager Layout
        => instance.layoutManager;

    private ColorSettings colorSettings;
    public static ColorSettings Colors
        => instance.colorSettings;

    private ImageBank images;
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
        this.fileManager = new FileManager();
        this.displayManager = new DisplayManager();
        this.layoutManager = new LayoutManager();
        this.colorSettings = new ColorSettings();
        this.images = new ImageBank();
    }

    public static bool Initialized => instance != null;
}
