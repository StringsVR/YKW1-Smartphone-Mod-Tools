using ImGui.Forms;
using ImGui.Forms.Controls;
using ImGui.Forms.Controls.Layouts;
using ImGui.Forms.Controls.Menu;
using ImGui.Forms.Factories;
using ImGui.Forms.Models.IO;
using System.Numerics;
using ImGui.Forms.Modals.IO;
using ImGui.Forms.Models;
using YKW1_Smartphone_Mod_Tools;
using SharpGen.Runtime.Win32;

//Hours wasted trying to optimize: 4
public class MainForm : Form
{
    public static Label fileLocationLabel;
    public static ProgressBar progressBar;
    public static MenuBarButton modeOption;

    public MainForm() : base()
    {
        FontFactory.RegisterFromFile("Roboto", @"Resources\roboto.ttf");
        this.Title = "YKW1 Smartphone Mod Tools";
        this.Size = new System.Numerics.Vector2(450, 680);

        this.MenuBar = new MainMenuBar();

        //  Adding Menu Bar

        //Add File Bar
        var FileMenuItems = new List<string> { "Install ADB", "Import via ADB", "Import App Package", "Export Package" };
        var FileMenuFunctions = new List<Action> { ClickHandling.installADB_Click , ClickHandling.importPackageViaADB_Click, ClickHandling.importPackageBtn_Click, ClickHandling.exportButton_Click };
        var FileMenu = AddUI.AddMenuItems("File", FileMenuItems, FileMenuFunctions);
        this.MenuBar.Items.Add(FileMenu);

        //Add Options Bar
        var OptionItems = new List<string> { "Reset To Preset", "Open Decompiled Folder" };
        var OptionItemsFunction = new List<Action> { ClickHandling.ResetAsync, ClickHandling.OpenDecompFolder };
        var OptionMenu = AddUI.AddMenuItems("Options", OptionItems, OptionItemsFunction);

        modeOption = new MenuBarButton("MODE: SPLIT_ASSET");
        modeOption.Clicked += (s, e) => ClickHandling.ChangeMODE();

        OptionMenu.Items.Add(modeOption);
        this.MenuBar.Items.Add(OptionMenu);

        //Add Exit
        var exitButton = new MenuBarButton("Exit");
        exitButton.Clicked += (s, e) => Environment.Exit(0);
        this.MenuBar.Items.Add(exitButton);

        //  Adding Layout

        // Define title layout
        var titleLabel = AddUI.MakeLabel("Import App Package", FontFactory.Get("Roboto", 35));

        var importPackageBtn = AddUI.MakeButton("Import Package", 26);
        importPackageBtn.Clicked += (s, e) => ClickHandling.importPackageBtn_Click();

        fileLocationLabel = AddUI.MakeLabel("d", FontFactory.GetDefault(26));
        Logic.SetFileLocation(null);

        var importTitleLayout = AddUI.createImportTitleLayout(titleLabel, fileLocationLabel, importPackageBtn);

        // Define decompile layout
        var decompileLabel = AddUI.MakeLabel("Decompile Package", FontFactory.Get("Roboto", 35));

        var decompilePackageBtn = AddUI.MakeButton("  Decompile  ", 26);
        decompilePackageBtn.Clicked += (s, e) => ClickHandling.decompilePackageBtn_ClickAsync();

        var decompileTitleLayout = AddUI.createTitleLayout(decompileLabel, decompilePackageBtn);

        // Define modding layout
        var moddingLabel = AddUI.MakeLabel("Modding", FontFactory.Get("Roboto", 35));

        var importModButton = AddUI.MakeButton(" Import Mod (*.ykm) ", 26);
        importModButton.Clicked += (s, e) => ClickHandling.ImportModBtn_Click();

        var injectModButton = AddUI.MakeButton(" Inject ", 26);
        injectModButton.Clicked += (s, e) => ClickHandling.InjectModBtn_Click();

        var modTitleLayout = AddUI.createTitleLayout(moddingLabel, importModButton, injectModButton);

        // Define build package layout
        var buildLabel = AddUI.MakeLabel("Build Package", FontFactory.Get("Roboto", 35));

        var buildButton = AddUI.MakeButton("   Build   ", 26);
        buildButton.Clicked += static (s, e) => ClickHandling.buildButton_Click();

        var mergeButton = AddUI.MakeButton("   Merge   ", 26);
        mergeButton.Clicked += static async (s, e) => await ClickHandling.mergeButton_ClickAsync();

        var signButton = AddUI.MakeButton("   Sign   ", 26);
        signButton.Clicked += static async (s, e) => await ClickHandling.signButton_ClickAsync();

        var buildTitleLayout = AddUI.createTitleLayout(buildLabel, buildButton, mergeButton, signButton);

        //Add Padding for Progress Stack
        var paddedLayout = new StackLayout { Size = new Size(SizeValue.Parent, SizeValue.Relative(0.03f)) };

        //Define Progress Stack
        var progressLayout = new StackLayout { Size = new Size(SizeValue.Parent, SizeValue.Relative(.05f)) };
        progressBar = new ProgressBar { Value = 0, Size = new Size(SizeValue.Relative(1f), SizeValue.Relative(1f)), Minimum = 0, Maximum = 100 };
        progressLayout.Items.Add(progressBar);

        //Define Export Stack
        var exportButton = AddUI.MakeButton("Export", 26);
        exportButton.Clicked += (s, e) => ClickHandling.exportButton_Click();

        var exportViaADButton = AddUI.MakeButton("Export Via .ADB", 26);
        exportViaADButton.Clicked += (s, e) => ClickHandling.exportViaADB_Click();

        var exportStackLayout = new StackLayout { Size = new Size(SizeValue.Parent, SizeValue.Relative(0.075f)) };
        exportStackLayout.Items.Add(AddUI.createButtonLayout(exportButton, exportViaADButton));

        this.Content = new StackLayout
        {
            Items =
            {
                importTitleLayout,
                decompileTitleLayout,
                modTitleLayout,
                buildTitleLayout,
                paddedLayout,
                progressLayout,
                exportStackLayout
            }
        };
    }

    private void ImportBtn_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
class Program
{
    public static Application app = new Application();
    static void Main()
    {
        var form = new MainForm();
        app.Execute(form);
    }
}
