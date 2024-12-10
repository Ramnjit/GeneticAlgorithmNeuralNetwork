using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text m_bestFitnessText;
    public Text m_currentFitnessText;
    public Text m_livesText;
    public Text m_generationNumberText;

    private void Update()
    {
        m_bestFitnessText.text = GameManager.singleton.GetBestFitness().ToString();        

        m_currentFitnessText.text = GameManager.singleton.GetCurrentFitness().ToString();
  
        GameManager.m_currentFitnessList.Add(m_currentFitnessText.text);

        m_livesText.text = GameManager.singleton.GetNumberOfBirds().ToString();
        GameManager.m_numberOfBirdsList.Add(m_livesText.text);
        
        m_generationNumberText.text = GeneticManger.singleton.GetGenerationNumber().ToString();
    }
}
