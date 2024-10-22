using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestScript
{
    private GameObject uiManagerGameObject;
    private UIManagerScript uiManager;
    private GameManagerScript gameManager;

    [SetUp]
    public void SetUp()
    {
        // Create a GameObject to attach the UIManagerScript to
        uiManagerGameObject = new GameObject();
        uiManager = uiManagerGameObject.AddComponent<UIManagerScript>();

        // Create a GameManagerScript instance and set up necessary properties
        gameManager = uiManagerGameObject.AddComponent<GameManagerScript>();
        gameManager.isGameActive = true; // Simulate that the game is active
        gameManager.player1 = "Player 1";
        gameManager.player2 = "Player 2";
        gameManager.currentPlayer = gameManager.player1;

        // Create UI Text objects to display the timer and matchup
        uiManager.gameTimerTextbox = new GameObject().AddComponent<Text>();
        uiManager.matchupTextbox = new GameObject().AddComponent<Text>();

        // Link the GameManager to UIManager
        uiManager.gameManager = gameManager;

        // Initialize the UIManager
        uiManager.TestStart();
    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GameTimerReflectsFiveMinutes()
    {
        // 5 minutes = 300 seconds
        float duration = 300f;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            uiManager.TestUpdate();
            elapsed += Time.deltaTime; 
            yield return null;
        }

        Assert.AreEqual("5:00", uiManager.gameTimerTextbox.text, "game timer should equal 5 minutes");
    }

    [UnityTest]
    public IEnumerator GameTimerNotInRightFormat()
    {
        // Simulate 5 minutes of game time (5 minutes = 300 seconds)
        float duration = 300f;

        // Run the Update method for the duration
        float elapsed = 0f;
        while (elapsed < duration)
        {
            uiManager.TestUpdate(); // Call Update to simulate the game loop
            elapsed += Time.deltaTime; // Simulate the time passing
            yield return null; // Wait for the next frame
        }

        // After 5 minutes, check if the timer reflects 5 minutes correctly
        Assert.AreNotEqual("300", uiManager.gameTimerTextbox.text, "game timer shouldn't show 300 (seconds), as its displayed in minutes:seconds");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(uiManagerGameObject);
    }
}
