﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeText : MonoBehaviour
{
    public Text displayText; //the text that is displayed
    public static int counter; //counter used for change the information on the menu
    public TextMesh button; //the text displayed on the button
    List<string> list = new List<string>(); //list for the instructions
    private GameObject checkbox; //the checkbox
    CheckboxInteraction checkboxInteraction; //used for changing the variable in CheckboxInteraction
    GameObject timerObj; //the timer obj
    Timer timer; //used for changing variable in Timer

    public void Start()
    {
        displayText.text = "Press Begin";
        button.text = "Begin";
        counter = 0;
        readData();
        checkbox = GameObject.FindGameObjectWithTag("checkbox");
        checkbox.SetActive(false); //checkbox will be deactivated on start
        checkboxInteraction = checkbox.GetComponent<CheckboxInteraction>();
        timerObj = GameObject.FindGameObjectWithTag("timer");
        timer = timerObj.GetComponent<Timer>();
    }

    public void Test() //will get called when the button is pressed
    {
        button.text = "Next";

        //the value of counter will determine which screen is displayed. More can be added, just create more cases.
        switch (counter)
        {
            case 1:
                displayText.text = "Is the Scene Safe?" + "\n" + "Personal Protective Equipment" + "\n" + "Check Number of Patients" + "\n" + "Medical or Trauma";
                counter++;
                break;
            case 2:
                displayText.text = "AVPU" + "\n" + "Check Carotid & Breathing for 5-10 secs" + "\n" + "Immediate High-Quality Compressions" + "\n" + "Announce 1st & every 20th compression" + "\n" + "Depth of 2 inches" + "\n" + "Change every 200th compression";
                counter++;
                break;
            case 3:
                displayText.text = "Open the Airway" + "\n" + "Clear Airway as Needed" + "\n" + "Assists Ventilations with BVM" + "\n" + "Provide High Flow Oxygen to BVM";
                counter++;
                break;
            case 4:
                displayText.text = "Choose Correct Size" + "\n" + "Test Cuff(King)" + "\n" + "Lubricate Distal End of Tube" + "\n" + "Perform a Tongue-Jaw Lift (King)" + "\n" + "Insert Tube into Corner of Mouth";
                counter++;
                break;
            case 5:
                displayText.text = "Blue Line Should Face the Chin (King)" + "\n" + "Colored Adapter at the Level of Lips(King)" + "\n" + "Inflate Cuff(King)" + "\n" + "Attach BVM";
                counter++;
                break;
            case 6:
                displayText.text = "Confirm Tube Placemen" + "\n" + "Measure and Insert OG (King)" + "\n" + "Suction OG/NG/Mouth as Needed" + "\n" + "Secure Tube to Patient";
                counter++;
                break;
            case 7:
                displayText.text = "Bear paitient's chest" + "\n" + "Apply Pads" + "\n" + "Turn on the defib" + "\n" + "Place QCPR Puck on Chest" + "\n" + "Coach compressor for depth,rate,& recoil";
                counter++;
                break;
            case 8:
                displayText.text = "Ready Next Compressor" + "\n" + "Palpate Femoral Pulse from 180th-200th Compressions" + "\n" + "Analyze at 200th Compression";
                counter++;
                break;
            case 9:
                displayText.text = "Clear All Personnel" + "\n" + "Shock as advised" + "\n" + "Immediately Direct Next Compressor to Begin";
                counter++;
                break;
            default:
                displayText.text = "Press Begin"; //assuming you've been through the whole menu, counter will reset to 1
                button.text = "Begin";
                counter = 1;
                break;
        }
    }
    public void Next() //In truth, this function isn't necessary. But it makes the keywords in the inspector easier to follow. 
    {
        if (SceneManager.GetActiveScene().name.Equals("CPR") || SceneManager.GetActiveScene().name.Equals("test"))
        {
            Test();
        }
        else
        {
            OneLine();
        }
    }

    public void GoBack()
    {
        counter -= 2; //since the counter is incremented in the switch case (or in OneLine), you have to subtract 2 in order to go back a page
        if (SceneManager.GetActiveScene().name.Equals("CPR") || SceneManager.GetActiveScene().name.Equals("test"))
        {
            Test();
        }
        else
        {
            OneLine();
        }
    }

    public void StartOver()//starts the app over, but seems to cause some issue with the audio listener. Need to look into it
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void readData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("cpr_insructions");
        string[] reader = textAsset.text.Split(new char[] { '\n' });
        string[] line = reader[0].Split(',');

        for (int i = 0; i < reader.Length; i++)
        {
            line = reader[i].Split(',');
            list.Add((line[0]));
        }
    }

    public void OneLine()
    {
        if (counter >= list.Count - 1 || counter < 0)
        {
            counter = 0;
            displayText.text = "Press Begin";
            button.text = "Begin";
            checkbox.SetActive(false);
            timer.timeElapsed = Mathf.RoundToInt(timer.timeElapsed); //round
            timer.timerText.text = "Time Elapsed: " + timer.timeElapsed.ToString(); //set the text
            timer.timeElapsed = 0; //reset timer
        }
        else
        {
            string progress = "\t  " + (counter+1).ToString() + "/" + (list.Count - 1).ToString();
            displayText.text = list[counter] + progress;
            checkboxInteraction.isChecked = false;
            button.text = "Next";
            counter++;
            checkbox.SetActive(true);
        }
    }

    public void CheckBoxTimer()
    {
        //will go to the next page 3 seconds after the checkbox is clicked
        checkboxInteraction.isChecked = true;
        Invoke("OneLine", 3);
    }
}
