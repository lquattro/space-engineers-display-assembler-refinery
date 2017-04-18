/** 
 * Constants 
 */ 
const string tagScript = "[ARS]"; 
const string tagClock = "CLOCK"; 
const string tagRefinery = "REFINERY"; 
const string tagAssembler = "ASSEMBLER"; 

public void Main(string argument) { 
 
    // Show Clock Time  
    TimeSpan timeOffset = new TimeSpan(0, 0, 0, 0);        
    DateTime currentTime = DateTime.Now;        
    DateTime currentTimeActual = currentTime.Add(timeOffset);   
    string timeClock = currentTimeActual.ToString("HH:mm:ss");    
 
    /**  
     * Variables texts  and color
     */  
    string textClock = "                                    Clock: "; 
    string textAssembler = "---==Assembler==---"; 
    string textRefinery = "---==Refinery==---"; 
    Color colorRed = new Color(255, 0, 0);  
    Color colorYellow = new Color(205, 205, 0);  
    Color colorGreen = new Color(0, 255, 0);  
 
    // LCD Panels 
    List<IMyTextPanel> lcds = new List<IMyTextPanel>();     
    GridTerminalSystem.GetBlocksOfType<IMyTextPanel>(lcds); 
 
    // Lights 
    List<IMyLightingBlock> lights = new List<IMyLightingBlock>();     
    GridTerminalSystem.GetBlocksOfType<IMyLightingBlock>(lights); 
 
    // Get All Assembler  
    List<IMyAssembler> assem = new List<IMyAssembler>();   
    GridTerminalSystem.GetBlocksOfType<IMyAssembler>(assem);     
  
    // Get All refinery  
    List<IMyRefinery> refin = new List<IMyRefinery>();    
    GridTerminalSystem.GetBlocksOfType<IMyRefinery>(refin);     
   
    // Look if assemblers is producing and return boolean  
    bool bAss0 = false;  
    bool bAss1 = false;
    foreach ( IMyAssembler ass in assem ) { 
        if ( ass.IsProducing == true ) {  
            bAss1 = true;  
        } else { 
            bAss0 = true; 
        } 
        textAssembler += "\n" + ass.CustomName + " work: " + ass.IsProducing;

        if ( ass.IsProducing ) { 
            ass.GetQueue( queue );
            //IMyProductionBlock bloc = (IMyProductionBlock) ass.TryGetQueueItem(0);
            textAssembler +=  " -> " + queue[0].BlueprintId.ToString().Split('/')[1] + ": " + ( (float) queue[0].Amount - (float) ass.CurrentProgress );
        }
    }  
  
    // Look if refinery is producing and return boolean  
    bool bRef0 = false; 
    bool bRef1 = false; 
    foreach ( IMyRefinery reff in refin ) { 
        if ( reff.IsProducing == true ) { 
            bRef1 = true; 
        } else { 
            bRef0 = true;  
        }  
        textRefinery += "\n" + reff.CustomName + " work: " + reff.IsProducing;   
    }  
 
    Color refineriesColor; 
    // Test activity Refinery and set lightColor 
    if ( bRef1 == true && bRef0 == false ) { 
        refineriesColor = colorGreen; 
    } else if (  bRef1 == true && bRef0 == true ) { 
        refineriesColor = colorYellow; 
    } else { 
        refineriesColor = colorRed; 
    } 
 
    Color assemblersColor; 
    // Set color for activity Assembler  
    if ( bAss1 == true && bAss0 == false ) {  
        assemblersColor = colorGreen; 
    } else if (  bAss1 == true && bAss0 == true ) {  
        assemblersColor = colorYellow; 
    } else {  
        assemblersColor = colorRed; 
    } 
 
    // Set Dictorary Map for light color 
    Dictionary<string,Color> lightModule = new Dictionary<string,Color> { 
             {tagAssembler, assemblersColor}, {tagRefinery, refineriesColor} }; 
 
    // Set Lightning color in light 
    foreach ( IMyLightingBlock light in lights ) { 
        // Exit if not contain tag mod 
        string lightName = light.CustomName; 
        if ( !lightName.Contains(tagScript) ) { 
            continue; 
        } 
 
        string[] modules = lightName.Split(';'); 
        Color color; 
        // Write Text with order tag nameLCD 
        foreach ( string module in modules ) { 
            if ( !lightModule.TryGetValue( module.ToUpper(), out color ) ) continue; 
            light.SetValue( "Color", color );  
        } 
    } 

    // Write Text Clock 
    textClock += timeClock;
 
    // Set Dictorary Map for data :  Module to Text 
    Dictionary<string,string> dataModule = new Dictionary<string,string> { {tagClock, textClock}, 
             {tagAssembler, textAssembler}, {tagRefinery, textRefinery} }; 
 
    // Set Text in all LCD panels 
    foreach ( IMyTextPanel lcd in lcds ) { 
        // Exit if not contain tag mod 
        string lcdName = lcd.CustomName; 
        if ( !lcdName.Contains(tagScript) ) { 
            continue; 
        } 
         
        string[] modules = lcdName.Split(';'); 
        string text = ""; 
        string outText = ""; 
        // Write Text with order tag nameLCD 
        foreach ( string module in modules ) { 
            if ( !dataModule.TryGetValue( module.ToUpper(), out outText ) ) continue; 
            text += outText + "\n"; 
        } 
 
        // Write test in displays 
        lcd.WritePublicText(text, false); 
        // Show public text in displays  
        lcd.ShowPublicTextOnScreen(); 
    } 
}
