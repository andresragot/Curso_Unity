using UnityEditor.Compilation;
using UnityEngine;

public class Loop : MonoBehaviour
{
    public int number = 0;

    [SerializeField]
    private int _priv_number = 0;

    // Getter and Setter
    public int Priv_Number
    {
        get { return _priv_number; }
        set
        {
            if (value >= 0 && value <= 10)
            {
                _priv_number = value;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int priv = Priv_Number;
        Priv_Number = 11;
    }

    // Update is called once per frame
    void Update()
    {
        // Vamos a comparar algo
        if (number == 5)
        {
            Debug.Log("Number is 5!");
        }
        else if (number > 5)
        {
            Debug.Log("Number: " + number + " is more than 5!");
        }
        else if (number < 5)
        {
            Debug.Log("Number: " + number + " is less than 5!");
        }

        // Este es más rapido que el if-else porque va directo al caso.
        // Lo malo es que no se puede evaluar casos menos especificos.
        switch (number)
        {
            case 5:
                Debug.Log("Switch case 5!");
                break;
            case 6:
                Debug.Log("Switch case 6!");
                break;

            default:
                Debug.Log("Switch dont know!");
                break;
        }

        // Bucles
        for (int i = 0; i < 10; i++)
        {
            Debug.Log("For: " + i);
        }

        int j = 0;
        while (j < 10)
        {
            Debug.Log("While: " + j);
            j++;
        }

        int k = 0;
        do
        {
            Debug.Log("Do While: " + k);
            k++;
        } while (k < 10);
    }
}
