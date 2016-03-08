//C# Example
using UnityEditor;
using UnityEngine;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;
using AssemblyCSharp.Level;

public class LevelEditor : EditorWindow
{
    public static LevelEditor Instance { get; private set; }
    public static bool IsOpen {
        get { return Instance != null; }
    }
    void OnEnable() {
        Instance = this;
    }

    bool groupEnabled;

    bool screenPlayFEnable;
    string screenPlayFile = "C:\\Users\\Admin\\Desktop\\Level\\scnply1.xml";
    
    bool levelFEnable;
    string levelFile = "Level File";

    static public GUIStyle activeStyle = new GUIStyle();
    static Texture2D img = null;
    
    // Add menu item named "Level Editor" to the Window menu
    [MenuItem("Tools/Level Editor")]
    public static void ShowWindow() {
        EditorWindow w = EditorWindow.GetWindow(typeof(LevelEditor));
        setupWindow();
        w.title = "Level Editor";
        w.Show();
    }

    public static void setupWindow(){
        img = Resources.Load<Texture2D>("black1x2i");
        
        activeStyle = new GUIStyle();
        activeStyle.border = new RectOffset(2,2,2,2);
        GUIStyleState slate = new GUIStyleState();
        slate.background = img;
        
        activeStyle.normal = slate;
        activeStyle.active = slate;
        activeStyle.focused = slate;
    }

    [MenuItem("Tools/--Close Editor--")]
    public static void CloseWindow() {

        if(LevelWindow.IsOpen){
            EditorWindow.GetWindow(typeof(LevelWindow)).Close();
        }

        if(ItemsWindow.IsOpen){
            EditorWindow.GetWindow(typeof(ItemsWindow)).Close();
        }

        if(LevelEditor.IsOpen){
            EditorWindow.GetWindow(typeof(LevelEditor)).Close();
        }

    }

    void saveScreenPlay() {
        TextLoader.saveFile(screenPlayFile, getScreenPlayText());
    }

    string getScreenPlayText() {
        DialogTree dt = new DialogTree(dNodes, dContent);
        dt.lastId = nextId;
        string xml = TextLoader.saveAsXml(dt);
        return xml;
    }

    void loadScreenPlay() {
        DialogTree dt = TextLoader.loadXmlFile<DialogTree>(screenPlayFile);
        resetDialogContent();
        resetDialogNodes();

        dNodes.AddRange(dt.nodes);
        foreach(DialogContent dc in dt.content){
            addDialogContent(dc);
        }
        nextId = dt.lastId;
    }

    void OnGUI()
    {
        if(img == null){
            setupWindow();
        }
        // screen play file
        screenPlayFEnable = EditorGUILayout.BeginToggleGroup ("Screen Play File", screenPlayFEnable);
        screenPlayFile = EditorGUILayout.TextField ( screenPlayFile);
        EditorGUILayout.EndToggleGroup ();

        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(75));
        if(GUILayout.Button("Load")){
            // load file
            loadScreenPlay();
        }

        if(GUILayout.Button("Save", GUILayout.MaxWidth(100))){
            // save file
            saveScreenPlay();
        }
        EditorGUILayout.EndHorizontal();

        // level edit file
        levelFEnable = EditorGUILayout.BeginToggleGroup ("Level File", levelFEnable);
        levelFile = EditorGUILayout.TextField ( levelFile);
        EditorGUILayout.EndToggleGroup ();

        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(75));
        if(GUILayout.Button("Load")){
            // load file
        }

        if(GUILayout.Button("Save", GUILayout.MaxWidth(100))){
            // save file
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(200));
        EditorGUILayout.Separator();
        //-----------------------------
        if(GUILayout.Button("Items", GUILayout.MaxWidth(100))){
            ItemsWindow.ShowWindow();
        }
        //-----------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.Separator();
        //-----------------------------
        if(GUILayout.Button("Level", GUILayout.MaxWidth(100))){
            LevelWindow.ShowWindow();
        }
        //-----------------------------
        EditorGUILayout.Separator();
        EditorGUILayout.EndHorizontal();

        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();

        drawContentPane();

        
        EditorGUILayout.Space();

        drawNodePane();
        
        EditorGUILayout.EndHorizontal();
    }
    

    Vector2 dContenScroll;
    Dictionary<long, DialogContent> dContMap = new Dictionary<long, DialogContent>();
    List<DialogContent> dContent = new List<DialogContent>();
    bool useIds = true;

    void resetDialogContent(){
        dContenScroll = new Vector2();
        dContMap = new Dictionary<long, DialogContent>();
        dContent = new List<DialogContent>();
    }
    void addDialogContent(DialogContent dc) {
        
        dContent.Add(dc);
        dContMap.Add(dc.id, dc);
    }

    DialogContent getDialogContent(long contentId){
        try{
            return dContMap[contentId];
        }catch(KeyNotFoundException e){
            return null;
        }
    }
    
    void drawContentPane() {
        EditorGUILayout.BeginVertical();

        if(dContent==null){
            dContent = new List<DialogContent>();
        }

        GUILayout.Label ("Dialog Content", EditorStyles.boldLabel);

        // add button
        if(GUILayout.Button("Add", GUILayout.MaxWidth(100))){
            long id = getNextId();
            DialogContent dc = new DialogContent(id, "", "New Dialog"+id);
            addDialogContent(dc);
        }

        // colapse all
        if(GUILayout.Button("Colapse All", GUILayout.MaxWidth(100))){
            for(int i = 0; i < dContent.Count; ++i){
                dContent[i].isUnFolded = false;
            }
        }

        // delete button
        if(GUILayout.Button("Delete", GUILayout.MaxWidth(100))){
            deleteCheckedCont();
        }

        // use labels
        EditorGUILayout.Separator();
        if(GUILayout.Button(useIds?"Use Labels":"Use Ids", GUILayout.MaxWidth(75))){
            useIds = !useIds;
        }

        // all dialog content
        dContenScroll = EditorGUILayout.BeginScrollView(dContenScroll);
        EditorGUILayout.BeginVertical();
        for(int i = 0; i < dContent.Count; ++i){
            drawDContent((DialogContent)dContent[i], i);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();

        EditorGUILayout.EndVertical();
    }

    long nextId = 0;
    long getNextId() {
        return nextId++;
    }

    void drawDContent(DialogContent dcont, int i) {
        EditorGUILayout.BeginVertical();
        
        EditorGUILayout.Space();
        EditorGUILayout.Separator();

        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(100));
        dcont.isChecked = EditorGUILayout.Toggle(dcont.isChecked, GUILayout.ExpandWidth(false));
        dcont.isUnFolded = EditorGUILayout.Foldout(dcont.isUnFolded, useIds ? dcont.id.ToString() : dcont.name);
        EditorGUILayout.EndHorizontal();

        if(dcont.isUnFolded){
            EditorGUILayout.TextField ("Id: ", dcont.id.ToString(), GUILayout.MaxWidth(200));

            dcont.foldContent = EditorGUILayout.Foldout(dcont.foldContent, trim(dcont.content, 20));
            if(dcont.foldContent){
                dcont.content = EditorGUILayout.TextArea (dcont.content);
            }
            dcont.name = EditorGUILayout.TextField ("Name: ", dcont.name, GUILayout.MaxWidth(400),
                                                    GUILayout.ExpandWidth(false));
        }

        EditorGUILayout.EndVertical();
    }

    void deleteCheckedCont() {
        for(int i = 0; i < dContent.Count; ++i){
            if(dContent[i].isChecked){
                dContent.RemoveAt(i);

                deleteCheckedCont();
                break;
            }
        }
    }


    
    Vector2 dNodesScroll;
    List<DialogNode> dNodes = new List<DialogNode>();
    int nodeShowSelc = 0;

    void resetDialogNodes(){
        dNodesScroll = new Vector2();
        dNodes = new List<DialogNode>();
    }

    void drawNodePane() {
        EditorGUILayout.BeginVertical();
        
        if(dNodes==null){
            dNodes = new List<DialogNode>();
        }
        GUILayout.Label ("Dialog Nodes", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        // add button
        if(GUILayout.Button("Add", GUILayout.MaxWidth(100))){
            if(!addToCheckedNodes(dNodes)){
                dNodes.Add(new DialogNode(getNextId(), -1));
            }
        }

        // colapse all
        if(GUILayout.Button("Colapse All", GUILayout.MaxWidth(100))){
            for(int i = 0; i < dNodes.Count; ++i){
                dNodes[i].isUnFolded = false;
            }
        }
        EditorGUILayout.EndHorizontal();

        // delete button
        if(GUILayout.Button("Delete", GUILayout.MaxWidth(100))){
            deleteCheckedNodes(dNodes);
        }
        
        // use labels
        EditorGUILayout.Separator();
        //-----------------------------
        EditorGUILayout.BeginHorizontal( GUILayout.MaxWidth(100));
        GUILayout.Label("Show: ");
        nodeShowSelc = GUILayout.SelectionGrid(nodeShowSelc, new string[]{"Ids", "Name", "Dialog"}, 3);
        EditorGUILayout.EndHorizontal();
        //-----------------------------
        EditorGUILayout.Separator();
        
        // all dialog content
        dNodesScroll = EditorGUILayout.BeginScrollView(dNodesScroll, 
                                                       GUILayout.MinWidth(200), 
                                                       GUILayout.MaxWidth(400),
                                                       GUILayout.ExpandWidth(false));
        EditorGUILayout.BeginVertical(activeStyle);

        int x = 15;
        for(int i = 0; i < dNodes.Count; ++i){
            x = drawNode(dNodes[i], i, x);
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();



        EditorGUILayout.EndVertical();
    }

    int drawNode(DialogNode dNode, int i, int x) {
        EditorGUILayout.BeginVertical(activeStyle);


        EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(100));
        dNode.isChecked = EditorGUILayout.Toggle(dNode.isChecked, GUILayout.ExpandWidth(false));
        string label = dNode.id.ToString();
        if(nodeShowSelc == 1){
            DialogContent dc = getDialogContent(dNode.contentId);
            if(dc != null){
                label = dc.name;
            }else{
                label = "_ _ _";
            }
        }else if(nodeShowSelc == 2){
            DialogContent dc = getDialogContent(dNode.contentId);
            if(dc != null){
                label = trim(dc.content, 20);
            }else{
                label = "...";
            }
        }

        dNode.isUnFolded = EditorGUILayout.Foldout(dNode.isUnFolded, label);
        EditorGUILayout.EndHorizontal();

        if(dNode.isUnFolded){
            int x2 = 55;

            EditorGUILayout.TextField ("Id: ", dNode.id.ToString(), GUILayout.MaxWidth(200));
            dNode.contentId = tryLong(EditorGUILayout.TextField ("CId: ", dNode.contentId.ToString(),
                                                                 GUILayout.MaxWidth(200)));

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);  // indent
            EditorGUILayout.BeginVertical();
            for(int k = 0; k < dNode.children.Count; ++k){
                x2 = drawNode(dNode.children[k], k, x2);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            x = x + x2;
        }else{
            x = x + 18;
        }

        EditorGUILayout.EndVertical();
        return x;
    }

    void deleteCheckedNodes(List<DialogNode> dNodes) {
        for(int i = 0; i < dNodes.Count; ++i){

            if(dNodes[i].isChecked){
                dNodes.RemoveAt(i);
                
                deleteCheckedNodes(dNodes);
                break;
            }else{
                deleteCheckedNodes(dNodes[i].children);
            }
        }
    }

    bool addToCheckedNodes(List<DialogNode> dNodes) {
        bool added = false;
        for(int i = 0; i < dNodes.Count; ++i){
            
            if(dNodes[i].isChecked){
                added = true;
                long id = getNextId();
                dNodes[i].children.Add(new DialogNode(id, -1));
            }

            added = addToCheckedNodes(dNodes[i].children) || added;
        }
        return added;
    }

    string trim(string s, int len){
        if(s.Length > len){
            return s.Substring(0, len);
        }
        return s.Substring(0, s.Length);
    }

    long tryLong(string str){
        long mylong = -1;
        if(long.TryParse(str,out mylong)){
            
            return mylong;
        }else{
            return -1;
        }
    }

}