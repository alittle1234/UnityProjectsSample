
using System;
using UnityEngine;
using AssemblyCSharp.Level;
using System.Collections.Generic;


namespace AssemblyCSharp {
    public class GuiDrawDialogTree : GuiDrawButtons {

        public GuiDrawDialogTree() {
        }
        
        string screenPlayFile = "C:\\Users\\Admin\\Desktop\\Level\\scnply1.xml";
        List<DialogNode> parentNodes = new List<DialogNode>();
        List<DialogContent> dialogContent = new List<DialogContent>();
        Dictionary<long, DialogContent> dialogMap = new Dictionary<long, DialogContent>();
        long nextId = 0; // keep an id to map dialog content. quick access

        
        float horzX = 5;
        float vertY = 5;
        float nodeHeight = 40;
        float nodeWidth = 100;
        float widthAdd = 0;
        float heightAdd = 0;

        void Start() {
            widthAdd = nodeWidth + 5;
            heightAdd = nodeHeight + 5;

            masterRef = new Vector3(0,0,1);
            loadScreenPlay();
            initialzeButtons();
        }

        long getNextId() {
            return nextId++;
        }
        /*
         * load from file
         * save to file
         * show all text nodes panel
         * search
         * collapse self
         * collapse children
         * save position data
         * edit text
         * add children
         * remove children
         * add script action
         * add prompt
         * add if logic, trigger
         * 
         * item prompts
         *  doors - move to room
         *  weapon, thing - pick up, inspect
         *  load items... make items, delte items, save to items
         * 
         * 
         * */
        void loadScreenPlay() {
            DialogTree dt = TextLoader.loadXmlFile<DialogTree>(screenPlayFile);
//            resetDialogContent();
//            resetDialogNodes();
            
            parentNodes.AddRange(dt.nodes);
            foreach(DialogContent dc in dt.content){
                addDialogContent(dc);
            }
            nextId = dt.lastId;
        }

        void addDialogContent(DialogContent dc) {
            dialogContent.Add(dc);
            dialogMap.Add(dc.id, dc);
        }

        void saveScreenPlay() {
            TextLoader.saveFile(screenPlayFile, getScreenPlayText());
        }
        
        string getScreenPlayText() {
            DialogTree dt = new DialogTree(parentNodes, dialogContent);
            dt.lastId = nextId;
            string xml = TextLoader.saveAsXml(dt);
            return xml;
        }
        


        void initialzeButtons(){
            foreach(DialogNode node in parentNodes){
                vertY += heightAdd;
                makeNodeButton(node, horzX, vertY, Color.black);
            }
        }

        GuiButton makeNodeButton(DialogNode node, float horz, float vert, Color buttonColor) {
            // make the button
            // check if button has saved horz, vert
            GuiButton button = new GuiButton(horz, vert, nodeHeight, nodeWidth,
                                             getDialogContentString(node.contentId),
                                             buttonColor,
                                             masterRef);
            myButtons.Add(button);

            // make children buttons
            if(node.children.Count>0) {
                Color childColor = getRandomColor();
                horzX = horzX + widthAdd;
                foreach(DialogNode child in node.children){
                    GuiButton childButton = makeNodeButton(child, horzX, vertY, childColor);
                    saveLine(
                        button,
                        childButton,
                        Color.black,
                        childColor);
                    vertY += heightAdd;
                }
                vertY -= heightAdd;
                horzX = horzX - widthAdd;
            }

            return button;
        }

        DialogContent getDialogContent(long contentId){
            try{
                return dialogMap[contentId];
            }catch(KeyNotFoundException e){
                return null;
            }
        }

        string getDialogContentString(long contentId){
            try{
                return getDialogContent(contentId).content;
            }catch(NullReferenceException e){
                return "---";
            }
        }

        float min = 0f;
        float max = 0.5f;
        UnityEngine.Random r = new UnityEngine.Random();
        Color getRandomColor() {
            float x = UnityEngine.Random.Range(min,max);
            float y = UnityEngine.Random.Range(min,max);
            float z = UnityEngine.Random.Range(min,max);
            return new Color(x,y,z);
        }
        
        static List<MyLine> myLines = new List<MyLine>();
        public class MyLine{
            public MyLine(GuiButton node, GuiButton child, Color lineColor, int width) {
                node1 = node;
                node2 = child;
                color = lineColor;
                this.width = width;
            }

            public GuiButton node1;
            public GuiButton node2;
            public Color color;
            public int width;
        }
        void saveLine(GuiButton node, GuiButton child, Color parentColor, Color childColor) {
            myLines.Add(new MyLine(node,child,parentColor, 3));
            myLines.Add(new MyLine(node,child,childColor, 1));
        }


        void OnGUI() {
            drawLines(myLines);
            base.drawThisGui();
        }

        void drawLine(MyLine line) {
            DrawLine.drawLine(
                new Vector2(line.node1.getXPos(), line.node1.getYPos()),
                new Vector2(line.node2.getXPos(), line.node2.getYPos()),
                line.color, line.width);
        }

        void drawLines(List<MyLine> lines){
            foreach(MyLine line in lines){
                drawLine(line);
            }
        }
    }
}

