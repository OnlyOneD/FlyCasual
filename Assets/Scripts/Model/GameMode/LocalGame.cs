﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameModes
{
    public class LocalGame : GameMode
    {
        public override void FinishTask()
        {
            Triggers.FinishTrigger();
        }

        public override void DeclareTarget(int thisShipId, int anotherShipId)
        {
            Combat.DeclareTarget(thisShipId, anotherShipId);
        }

        public override void NextButtonEffect()
        {
            UI.NextButtonEffect();
        }

        public override void SkipButtonEffect()
        {
            UI.SkipButtonEffect();
        }

        public override void ConfirmShipSetup(int shipId, Vector3 position, Vector3 angles)
        {
            (Phases.CurrentSubPhase as SubPhases.SetupSubPhase).ConfirmShipSetup(shipId, position, angles);
        }

        public override void PerformStoredManeuver(int shipId)
        {
            ShipMovementScript.PerformStoredManeuver(Selection.ThisShip.ShipId);
        }

        public override void AssignManeuver(int shipId, string maneuverCode)
        {
            ShipMovementScript.AssignManeuver(Selection.ThisShip.ShipId, maneuverCode);
        }

        public override void FinishMovementExecution()
        {
            Phases.CurrentSubPhase.Next();
        }

        public override void GiveInitiativeToRandomPlayer()
        {
            int randomPlayer = UnityEngine.Random.Range(1, 3);
            Phases.PlayerWithInitiative = Tools.IntToPlayer(randomPlayer);
            Triggers.FinishTrigger();
        }

        public override void ShowInformCritPanel()
        {
            InformCrit.ShowPanelVisible();
        }

        public override void StartBattle()
        {
            Global.StartBattle();
        }

        // BARREL ROLL

        public override void TryConfirmBarrelRollPosition(Vector3 shipBasePosition, Vector3 movementTemplatePosition)
        {
            (Phases.CurrentSubPhase as SubPhases.BarrelRollPlanningSubPhase).TryConfirmBarrelRollPosition();
        }

        public override void StartBarrelRollExecution(Ship.GenericShip ship)
        {
            (Phases.CurrentSubPhase as SubPhases.BarrelRollPlanningSubPhase).StartBarrelRollExecution(ship);
        }

        public override void FinishBarrelRoll()
        {
            (Phases.CurrentSubPhase as SubPhases.BarrelRollExecutionSubPhase).FinishBarrelRollAnimation();
        }

        // BOOST

        public override void TryConfirmBoostPosition(string selectedBoostHelper)
        {
            (Phases.CurrentSubPhase as SubPhases.BoostPlanningSubPhase).TryConfirmBoostPosition();
        }

        public override void StartBoostExecution(Ship.GenericShip ship)
        {
            (Phases.CurrentSubPhase as SubPhases.BoostPlanningSubPhase).StartBoostExecution(ship);
        }

        public override void FinishBoost()
        {
            Phases.FinishSubPhase(typeof(SubPhases.BoostExecutionSubPhase));
        }

        public override void UseDiceModification(string effectName)
        {
            Combat.UseDiceModification(effectName);
        }

        public override void ConfirmDiceResults()
        {
            Combat.ConfirmDiceResultsClient();
        }

        public override void GetCritCard(Action callBack)
        {
            int[] randomHolder = new int[1];
            randomHolder[0] = UnityEngine.Random.Range(0, CriticalHitsDeck.GetDeckSize());
            CriticalHitsDeck.SetCurrentCriticalCardByIndex(randomHolder);
            callBack();
        }

        public override void TakeDecision(KeyValuePair<string, EventHandler> decision, GameObject button)
        {
            decision.Value.Invoke(button, null);
        }
    }
}