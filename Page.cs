using System;
using System.Collections.Generic;
using System.Text;
using BananaOS;
using BananaOS.Pages;
using BepInEx;
using Utilla;
using UnityEngine;

namespace BananaOSLongarms
{

    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    class LongArmPage : WatchPage
    {
        
        
        // Stuff I Added.
        public float armLength = 1.2f;
        public string longArmTypeName = "Steam Long Arms";
        public int longArmTypeIndex = 0;
        public int maxLongArmTypeIndex = 2;
        public static bool multiArms = false;
        public static bool vertArms = false;

        // Huskys Stupid Shit.
        public override string Title => "<color=blue>==</color> Banana Arms <color=blue>==</color>";
        public override bool DisplayOnMainMenu => true;

        public override void OnPostModSetup()
        {
            selectionHandler.maxIndex = 3;
        }


        public override string OnGetScreenContent()
        {
            if (longArmTypeIndex == 2)
            {
                longArmTypeName = "To Be Added.";
            }
            if (longArmTypeIndex == 1)
            {
                longArmTypeName = "Multiplied Long Arms";
            }
            if (longArmTypeIndex == 0)
            {
                longArmTypeName = "Steam Long Arms";
            }
            StringBuilder str = new StringBuilder();
            if (!Main.inRoom)
            {
                str.AppendLine("<color=red>You need to be in a modded lobby.</color=red>");
            }
            else
            {
                str.AppendLine("<color=red></color> Banana Arms \n\n <color=red></color>");
                str.AppendLine("<color=yellow></color> Use left and right to change length. <color=yellow></color>");
                str.AppendLines(1);
                str.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "Enable Banana Arms"));
                str.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(1, "Disable Banana Arms"));
                str.AppendLine("<color=green></color>\n              -----Settings-----\n <color=green></color>");
                str.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(2, "Long Arm Type: " + longArmTypeName));
                str.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(3, $"Long Arm Length: {armLength}"));


            }
            

            return str.ToString();
        }

        public void onRight()
        {
            int index = selectionHandler.currentIndex;

            if (index == 2)
            {
                if (longArmTypeIndex != maxLongArmTypeIndex)
                {
                    longArmTypeIndex++;
                }
                else
                {
                    longArmTypeIndex = 0;
                }
            }
            if (index == 3)
            {
                armLength += 0.1f;
            }
        }
        public void onLeft()
        {
            int index = selectionHandler.currentIndex;
            if (index == 2)
            {
                if (longArmTypeIndex != 0)
                {
                    longArmTypeIndex--;
                }
                else
                {
                    longArmTypeIndex = maxLongArmTypeIndex;
                }

            }
            if (index == 3)
            {
                armLength += -0.1f;
            }
        }
        public void multarm()
        {
            GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - (GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.leftHandTransform.position) * armLength;
            GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - (GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.rightHandTransform.position) * armLength;
        }
        public void vertarm()
        {
            Vector3 lefty = GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.leftHandTransform.position;
            lefty.y *= armLength;
            Vector3 righty = GorillaTagger.Instance.headCollider.transform.position - GorillaTagger.Instance.rightHandTransform.position;
            righty.y *= armLength;
            GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - lefty;
            GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position = GorillaTagger.Instance.headCollider.transform.position - righty;
        }
        public void Update()
        {
            if (multiArms)
            {
                multarm();
            }
            if (vertArms)
            {
                vertarm();
            }

        }
        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;
                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;
                case WatchButtonType.Right:
                    onRight();
                    break;
                    
                case WatchButtonType.Left:
                    onLeft();
                    break;
                case WatchButtonType.Enter:
                    if (Main.inRoom)
                    {


                        int index = selectionHandler.currentIndex;
                        if (index == 0 && Main.inRoom && longArmTypeIndex == 0)
                        {
                            multiArms = false;
                            vertArms = false;
                            GorillaLocomotion.Player.Instance.transform.localScale = new UnityEngine.Vector3(armLength, armLength, armLength);
                        }
                        else if (index == 1)
                        {
                            GorillaLocomotion.Player.Instance.transform.localScale = Vector3.one;
                            multiArms = false;
                            vertArms = false;
                        }
                        else if (index == 0 && Main.inRoom && longArmTypeIndex == 1)
                        {
                            multiArms = true;
                        }
                        /*else if (index == 0 && Main.inRoom && longArmTypeIndex == 2)
                        {
                            vertArms = true;
                            GorillaLocomotion.Player.Instance.transform.localScale = Vector3.one;
                            multiArms = false;
                        }*/
                    }

                    break;
                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
    }
}
