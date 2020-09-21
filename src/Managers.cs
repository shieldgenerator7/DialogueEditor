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
    {
        get => instance.nodeManager;
    }

    private ControlManager controlManager;
    public static ControlManager Control
    {
        get => instance.controlManager;
    }

    private DisplayManager displayManager;
    public static DisplayManager Display
    {
        get => instance.displayManager;
    }

    public static frmMain Form
    {
        get => instance.mainForm;
    }

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
}
