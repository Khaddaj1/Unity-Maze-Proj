using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject _FrontWall;

    [SerializeField]
    private GameObject _leftWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _BackWall;

    [SerializeField]
    private GameObject _UnVistedBlock;

    // Temporary reference for holding the wall during swaps
    private GameObject tempWall;

    public bool IsVisited { get; private set; }
    // If maze is visited 

    /*void Awake()
    {
        // Randomize walls at the start
        RandomizeWalls();
    }*/

    public void Visit()
    {
        // This will be called when Cell visited by generator
        IsVisited = true;
        _UnVistedBlock.SetActive(false);
    }

    public void ClearRightWall()
    {
        _rightWall.SetActive(false);
    }

    public void ClearLeftWall()
    {
        _leftWall.SetActive(false);
    }

    public void ClearFrontWall()
    {
        _FrontWall.SetActive(false);
    }

    public void ClearBackWall()
    {
        _BackWall.SetActive(false);
    }

    private void RandomizeWalls()
    {
        // Step 1: Randomly pick two walls to swap (we'll use front wall as the reference wall)
        int randomWallIndex = Random.Range(0, 4);
        GameObject wallToSwap = null;

        switch (randomWallIndex)
        {
            case 0:
                wallToSwap = _leftWall;
                SwapWallPositionAndRotation(_FrontWall, _leftWall);
                _FrontWall = _leftWall;
                _leftWall = wallToSwap;
                break;
            case 1:
                wallToSwap = _rightWall;
                SwapWallPositionAndRotation(_FrontWall, _rightWall);
                _FrontWall = _rightWall;
                _rightWall = wallToSwap;
                break;
            case 2:
                wallToSwap = _BackWall;
                SwapWallPositionAndRotation(_FrontWall, _BackWall);
                _FrontWall = _BackWall;
                _BackWall = wallToSwap;
                break;
            case 3:
                wallToSwap = _FrontWall;
                SwapWallPositionAndRotation(_FrontWall, _leftWall);
                _FrontWall = _leftWall;
                _leftWall = wallToSwap;
                break;
        }
    }

    private void SwapWallPositionAndRotation(GameObject wall1, GameObject wall2)
    {
        // Store position of the second wall (the one we are swapping with)
        Vector3 tempPosition = wall2.transform.position;
        
        // Swap positions
        wall2.transform.position = wall1.transform.position;
        wall1.transform.position = tempPosition;

        // Store rotation of wall2
        Quaternion tempRotation = wall2.transform.rotation;

        // Now adjust the rotation based on the wall being swapped
        Vector3 newRotation = wall1.transform.localRotation.eulerAngles;

        // Apply rotation logic to handle each wall type
        if (wall1 == _leftWall || wall1 == _rightWall)
        {
            newRotation.y += 180f; // Flip left and right walls by 180 degrees on Y-axis
        }
        else if (wall1 == _FrontWall || wall1 == _BackWall)
        {
            newRotation.y += 180f; // Flip front and back walls by 180 degrees on Y-axis
        }

        // Apply the new rotation for wall1 and swap the rotation for wall2
        wall2.transform.localRotation = Quaternion.Euler(newRotation);
        wall1.transform.localRotation = tempRotation;
    }
}
