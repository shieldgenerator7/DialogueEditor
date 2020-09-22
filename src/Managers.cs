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


    private DisplayManager displayManager;
    public static DisplayManager Display
        => instance.displayManager;

    private FileManager fileManager;
    public static FileManager File
        => instance.fileManager;

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
        this.displayManager = new DisplayManager();
    }

    public static bool Initialized => instance != null;
}
