using UnityEngine;

public class CombatSimulator : MonoBehaviour
{
    [Header("Unit Attributes")]
    [Tooltip("Maximum Health Points for a single unit.")]
    public int unitMaxHP = 100;
    [Tooltip("Damage dealt by a single unit per combat tick.")]
    public int unitDamage = 20;

    [Header("Army Setup")]
    [Tooltip("Initial number of attacking units.")]
    public int attackerCount = 1000;
    [Tooltip("Initial number of defending units.")]
    public int defenderCount = 800;

    [Header("System Rules")]
    [Range(0f, 1f)]
    [Tooltip("The deterministic ratio of lost units that are converted to 'Wounded' instead of 'Killed' (e.g., 0.3 = 30%).")]
    public float woundedRatio = 0.3f;

    [Header("Simulation Results (Read Only)")]
    public int attackerWounded = 0;
    public int defenderWounded = 0;

    // This attribute allows designers to run the simulation directly from the Inspector's context menu (right-click the script).
    [ContextMenu("Run Combat Simulation")]
    public void RunSimulation()
    {
        // 1. Initialization
        int currentAttackers = attackerCount;
        int currentDefenders = defenderCount;
        attackerWounded = 0;
        defenderWounded = 0;
        int tick = 0;

        Debug.Log("<color=yellow>--- Combat Simulation Started ---</color>");

        // 2. Core Combat Loop: Runs until one side is completely wiped out.
        while (currentAttackers > 0 && currentDefenders > 0)
        {
            tick++;
            
            // Step A: Calculate simultaneous total damage dealt by both sides in this tick.
            int dmgToDefender = currentAttackers * unitDamage;
            int dmgToAttacker = currentDefenders * unitDamage;

            // Step B: Calculate unit losses. 
            // We use integer division (dmg / HP) to find full units destroyed.
            // Mathf.Min ensures we don't lose more units than we currently have (prevents negative counts).
            int attackerLosses = Mathf.Min(currentAttackers, dmgToAttacker / unitMaxHP);
            int defenderLosses = Mathf.Min(currentDefenders, dmgToDefender / unitMaxHP);

            // Step C: Apply the Deterministic Wounded System.
            // Instead of RNG (randomness), a fixed percentage of losses becomes 'Wounded' to prevent frustrating edge cases.
            int aWounded = Mathf.RoundToInt(attackerLosses * woundedRatio);
            int aKilled = attackerLosses - aWounded;

            int dWounded = Mathf.RoundToInt(defenderLosses * woundedRatio);
            int dKilled = defenderLosses - dWounded;

            // Step D: Apply the results to the current army counts and accumulate total wounded.
            currentAttackers -= attackerLosses;
            currentDefenders -= defenderLosses;
            attackerWounded += aWounded;
            defenderWounded += dWounded;

            // Step E: Output detailed tick logs for the balancing designer to review.
            Debug.Log($"[Tick {tick}] Attackers: {attackerLosses} Lost (<color=red>{aKilled} KIA</color>, <color=green>{aWounded} Wounded</color>) | Remaining: {currentAttackers}");
            Debug.Log($"[Tick {tick}] Defenders: {defenderLosses} Lost (<color=red>{dKilled} KIA</color>, <color=green>{dWounded} Wounded</color>) | Remaining: {currentDefenders}");

            // Failsafe: Prevent infinite loops in case of 0 damage scenarios.
            if (tick > 50) 
            {
                Debug.LogWarning("Simulation aborted: Reached 50 ticks to prevent infinite loop.");
                break;
            }
        }

        // 3. Final Summary Output (Monetization & Economy Link)
        Debug.Log("<color=cyan>--- Combat Simulation Ended ---</color>");
        Debug.Log($"Total Attacker Wounded: {attackerWounded} -> [Can be recovered at 40% of original cost / Speeds up combat iteration]");
        Debug.Log($"Total Defender Wounded: {defenderWounded} -> [Can be recovered at 40% of original cost / Speeds up combat iteration]");
    }
}