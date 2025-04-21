using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Projectile
{
    public class Modulator
    {
        [System.Serializable]
        public struct ModulatorProperties
        {
            public float amplitude;
            public float frequency;
            public float lift;
            public float offset;
        }

        public enum FunctionType
        {
            Sin,
            Cos
        }

        [System.Serializable]
        public struct ModulatorEntry
        {
            public FunctionType modulationType;
            public ModulatorProperties waveProperties;
        }

        private float ModulatorValueAt(ModulatorEntry modulator, float time)
        {
            var wave = modulator.waveProperties;
            if (modulator.modulationType == FunctionType.Sin)
                return Mathf.Sin(time * wave.frequency + wave.offset) * wave.amplitude + wave.lift;
            if (modulator.modulationType == FunctionType.Cos)
                return Mathf.Cos(time * wave.frequency + wave.offset) * wave.amplitude + wave.lift;

            Debug.Log("Couldn't get modulator value. No matching modulation type.");
            return 0;
        }

        public float SumOfModulatorValuesAt(List<ModulatorEntry> modulators, float time)
        {
            float total = 0;
            foreach (var modulator in modulators) 
            {
                total += ModulatorValueAt(modulator, time);
            }

            return total;
        }
    }
}

