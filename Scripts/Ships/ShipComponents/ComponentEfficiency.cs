/*ComponentEfficiency.cs (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: Contains the data and cacluation methods for enabling loss of efficiency on ship components
Created:  2021-07-29T18:42:48.833Z
Modified: 2021-07-29T20:48:04.736Z
*/

using System;
using BlueKnightOne.Ships.ShipComponents;
using Godot;

public class ComponentEfficiency : Resource
{
    public enum CalculationMethod
    {
        PercentTimesSumOfBasePlusModifier,
        PercentTimesBasePlusModifer,
        PercentTimesModifierPlusBase
    }
    
    [Export] public CalculationMethod calculationMethod = CalculationMethod.PercentTimesSumOfBasePlusModifier;
    [Export] public float NominalPenalty;
    [Export(PropertyHint.ExpRange, "0,100,0.01")] public float NominalPercentagePenalty;
    [Export] public float WornPenalty;
    [Export(PropertyHint.ExpRange, "0,100,0.01")] public float WornPercentagePenalty;
    [Export] public float DamagedPenalty;
    [Export(PropertyHint.ExpRange, "0,100,0.01")] public float DamagedPercentagePenalty;
    [Export] public float OverclockBonus;
    [Export(PropertyHint.ExpRange, "0,100,0.01")] public float OverclockPercentageBonus;

    public float CalculateEfficiencyModification(float baseValue, ShipComponentState componentState)
    {
        float modifier = -NominalPenalty;
        float percentModifer = -NominalPercentagePenalty;

        if ((componentState & ShipComponentState.Worn) != 0)
        {
            modifier -= WornPenalty;
            percentModifer -= WornPercentagePenalty;
        }

        if ((componentState & ShipComponentState.Damaged) != 0)
        {
            modifier -= DamagedPenalty;
            percentModifer -= DamagedPercentagePenalty;
        }

        if ((componentState & ShipComponentState.Overclock) != 0)
        {
            modifier += OverclockBonus;
            percentModifer += OverclockPercentageBonus;
        }

        switch (calculationMethod)
        {
            case CalculationMethod.PercentTimesSumOfBasePlusModifier:
                return percentModifer * (baseValue + modifier);
            case CalculationMethod.PercentTimesBasePlusModifer:
                return percentModifer * baseValue + modifier;
            case CalculationMethod.PercentTimesModifierPlusBase:
                return percentModifer * modifier + baseValue;
            default:
                throw new IndexOutOfRangeException();
        }
    }
}