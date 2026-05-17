# 4X Strategy System & Economy Prototyping Tools

This repository contains system design prototypes and custom balancing tools created to demonstrate technical game design, macro-economy balancing, and editor-tooling capabilities for strategy games. 

The project focuses on solving common pacing issues in 4X/Strategy genres, such as the early-game "Poverty Trap" and player sunk-cost aversion during combat.

## 📊 1. Macro-Economy Simulator (Excel)
**File:** `Economic_Simulator.xlsx`

A dynamic economic model designed to balance "Tall" (infrastructure-focused) and "Wide" (expansion-focused) gameplay strategies. 

### Key Design Features:
* **Break-even Analysis:** The baseline economy is tuned so that the Wide strategy's high initial cost and upkeep reach a break-even point with the Tall strategy around Day 3.
* **The "Quest Reward" Dynamic Lever:** Introduced a FSM-based Quest System to mitigate the "Poverty Trap" (where early combat losses permanently cripple the player's economy). 
* **Interactive Balancing:** The Excel sheet includes an interactive slider control. By adjusting the Quest Reward value, designers can visualize how it scales the economic snowball effect and incentivizes map expansion over static turtling. This acts as a powerful lever for Live Ops tuning.

---

## ⚔️ 2. Deterministic Combat & Wounded Simulator (Unity C#)
**Folder:** `Assets/Scripts/CombatSimulator.cs`

A custom Unity Editor tool built to simulate tick-based combat outcomes without needing to enter Play mode, drastically reducing iteration time for the balancing team.

### Key Design Features:
* **Deterministic Wounded System:** Replaced RNG-based casualty mechanics with a strict 30% deterministic wounded logic. By guaranteeing a fixed portion of lost units can be recovered (at ~40% of original production cost), it significantly reduces player sunk-cost aversion and encourages a higher frequency of PvP engagements.
* **Integer-Based Logic:** Utilizes integer division to calculate "completely destroyed" units per tick, preventing fractional unit deaths and ensuring mathematically sound casualty reports.
* **Fail-safes:** Built-in loop breakers prevent editor freezing during edge-case balance tests (e.g., 0 damage scenarios).

### How to Use the Tool in Unity:
1. Open `SimulatorScene.unity`.
2. Select the `Combat_Simulator` GameObject in the Hierarchy.
3. In the Inspector, adjust the parameters (Unit Max HP, Damage, Army Counts, Wounded Ratio).
4. **Right-click** the `Combat Simulator (Script)` component name (or click the three-dot menu).
5. Click **`Run Combat Simulation`**.
6. View the detailed tick-by-tick combat logs and final casualty reports in the **Console Window**.

---

### 🛠️ Tech Stack & Workflow
* **Design & Balancing:** Microsoft Excel (Form Controls, Dynamic Functions)
* **Prototyping & Tooling:** Unity (C#), `.NET 8.0 SDK`
* **Version Control:** Git / GitHub
