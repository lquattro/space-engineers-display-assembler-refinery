public Program() {
    // The constructor, called only once every session and
    // always before any other method is called. Use it to
    // initialize your script. 
    //     
    // The constructor is optional and can be removed if not
    // needed.
}

public void Save() {
    // Called when the program needs to save its state. Use
    // this method to save your state to the Storage field
    // or some other means. 
    // 
    // This method is optional and can be removed if not
    // needed.
}

public void Main(string argument) {
    // The main entry point of the script, invoked every time
    // one of the programmable block's Run actions are invoked.
    // 
    // The method itself is required, but the argument above
    // can be removed if not needed.
    /**
     * Constants
     */
    const string namePanels = "BC LCD ";
    const string nameLight = "BD LIGHT ";

   /** 
     * Variables 
     */
    Color colorRed = new Color(255, 0, 0);
    Color colorYellow = new Color(205, 205, 0);
    Color colorGreen = new Color(0, 255, 0);

    // LCD Panels 
    IMyTextPanel displayDL = GridTerminalSystem.GetBlockWithName( namePanels + 1 ) as IMyTextPanel;   
    IMyTextPanel displayDR = GridTerminalSystem.GetBlockWithName( namePanels + 2 ) as IMyTextPanel;  
 
    // Light 
    IMyLightingBlock light1 = GridTerminalSystem.GetBlockWithName( nameLight + 1 ) as IMyLightingBlock;
    IMyLightingBlock light2 = GridTerminalSystem.GetBlockWithName( nameLight + 2 ) as IMyLightingBlock;
 
    // Get All Assembler 
    List<IMyAssembler> blocklist = new List<IMyAssembler>();  
    GridTerminalSystem.GetBlocksOfType<IMyAssembler>(blocklist);    
 
    // Get All refinery 
    List<IMyRefinery> blocklist2 = new List<IMyRefinery>();   
    GridTerminalSystem.GetBlocksOfType<IMyRefinery>(blocklist2);    
 
    string textDL = "List Assembler\n---------------\n"; 
    string textDR = "List Refinery\n---------------\n"; 
  
    // Look if assemblers is producing and return boolean 
    IMyAssembler as1; 
    bool bAss0 = false; 
    bool bAss1 = false;
    for ( int i = 0; i < blocklist.Count; i++) { 
        as1 = blocklist[i]; 

        if ( as1.IsProducing == true ) { 
            bAss1 = true; 
        }  
        if ( as1.IsProducing == false ) {  
            bAss0 = true;  
        } 
 
        textDL += as1.CustomName + " work: " + as1.IsProducing + "\n"; 
    } 
 
    // Look if refinery is producing and return boolean 
    IMyRefinery ref1;
    bool bRef0 = false;
    bool bRef1 = false;
    for ( int i = 0; i < blocklist2.Count; i++) {  
        ref1 = blocklist2[i];

        if ( ref1.IsProducing == true ) {
            bRef1 = true;
        } 
        if ( ref1.IsProducing == false ) { 
            bRef0 = true; 
        } 
  
        textDR += ref1.CustomName + " work: " + ref1.IsProducing + "\n";  
    } 

    // Test activity Refinery and set lightColor
    if ( bRef1 == true && bRef0 == false ) {
        light2.SetValue("Color", colorGreen );
    } else if (  bRef1 == true && bRef0 == true ) {
        light2.SetValue("Color", colorYellow );
    } else {
        light2.SetValue("Color", colorRed );
    }

    // Test activity Assembler and set lightColor 
    if ( bAss1 == true && bAss0 == false ) { 
        light1.SetValue("Color", colorGreen ); 
    } else if (  bAss1 == true && bAss0 == true ) { 
        light1.SetValue("Color", colorYellow ); 
    } else { 
        light1.SetValue("Color", colorRed ); 
    }
 
    // Write text in displays 
    displayDL.WritePublicText(textDL, false); 
    displayDR.WritePublicText(textDR, false); 
 
    // Show public text in displa 
    displayDL.ShowPublicTextOnScreen(); 
    displayDR.ShowPublicTextOnScreen();
}