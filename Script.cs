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

    // LCD Panels
    IMyTextPanel displayDL = GridTerminalSystem.GetBlockWithName("BC LCD DL") as IMyTextPanel;  
    IMyTextPanel displayDR = GridTerminalSystem.GetBlockWithName("BC LCD DR") as IMyTextPanel; 

    // Light
    // IMyTextPanel cornerLightL = GridTerminalSystem.GetBlockWithName("BD-Corner Light L") as IMyTextPanel; 
    // IMyTextPanel cornerLightR = GridTerminalSystem.GetBlockWithName("BD-Corner Light R") as IMyTextPanel; 

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
    for ( int i = 0; i < blocklist.Count; i++) {
        as1 = blocklist[i];

        textDL += as1.CustomName + " work: " + as1.IsProducing + "\n";
    }

    // Look if refinery is producing and return boolean
    IMyRefinery ref1; 
    for ( int i = 0; i < blocklist2.Count; i++) { 
        ref1 = blocklist2[i];
 
        textDR += ref1.CustomName + " work: " + ref1.IsProducing + "\n"; 
    }

    // Write text in displays
    displayDL.WritePublicText(textDL, false);
    displayDR.WritePublicText(textDR, false);

    // Show public text in displa
    displayDL.ShowPublicTextOnScreen();
    displayDR.ShowPublicTextOnScreen();
}
