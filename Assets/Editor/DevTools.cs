using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class DevTools : EditorWindow
    {
        private int giveCoinsAmount = 100;
        private UpgradeType selectedUpgrade;
        private int upgradeLevel = 1;

        [MenuItem("Tools/Idle Game Dev Tools")]
        public static void ShowWindow()
        {
            GetWindow<DevTools>("Idle Dev Tools");
        }

        private void OnGUI()
        {
            GUILayout.Label(" Developer Tools", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // Give Coins
            GUILayout.Label(" Give Coins");
            giveCoinsAmount = EditorGUILayout.IntField("Amount", giveCoinsAmount);

            if (GUILayout.Button("Give Coins"))
            {
                if (Application.isPlaying && GameManager.Instance != null)
                {
                    GameManager.Instance.AddCoins(giveCoinsAmount);
                    Debug.Log($"Added {giveCoinsAmount} coins");
                }
                else
                {
                    Debug.LogWarning("Game must be playing to give coins.");
                }
            }

            EditorGUILayout.Space();

            // Set Upgrade Level
            GUILayout.Label(" Set Upgrade Level");
            selectedUpgrade = (UpgradeType)EditorGUILayout.EnumPopup("Upgrade Type", selectedUpgrade);
            upgradeLevel = EditorGUILayout.IntField("Level", upgradeLevel);

            if (GUILayout.Button("Set Upgrade Level"))
            {
                if (Application.isPlaying && GameManager.Instance != null)
                {
                    GameManager.Instance.SetUpgradeLevel(selectedUpgrade, upgradeLevel);
                    Debug.Log($"Set {selectedUpgrade} to level {upgradeLevel}");
                }
            }

            EditorGUILayout.Space();

            // Reset Save
            GUILayout.Label(" Reset Save");

            if (GUILayout.Button("Delete Save File"))
            {
                SaveSystem.DeleteSave();
                Debug.Log("Save file deleted.");
            }

            EditorGUILayout.Space();

            // Coin Rate
            GUILayout.Label(" Coin Rate (AutoCollector)");

            if (Application.isPlaying && GameManager.Instance != null)
            {
                int level = GameManager.Instance.GetUpgradeLevel(UpgradeType.AutoCollector);
                float rate = GameManager.Instance.upgradeDefinitions[UpgradeType.AutoCollector].GetValue(level);
                EditorGUILayout.LabelField("Auto Coin Rate", $"{rate:0.##} / sec");
            }
            else
            {
                EditorGUILayout.HelpBox("Start the game to see live values.", MessageType.Info);
            }
        }
    }
}
