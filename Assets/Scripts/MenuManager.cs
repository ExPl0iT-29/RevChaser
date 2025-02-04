using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _newBestText;

    private void Awake() {
        if (GameManager.Instance.IsInitialized) {
            StartCoroutine(ShowScore());
        } else {
            _scoreText.gameObject.SetActive(false);
            _newBestText.gameObject.SetActive(false);   
            _highScoreText.text = GameManager.Instance.HighScore.ToString();
        }
    }

    [SerializeField] private float _animationTime;
    [SerializeField] private AnimationCurve _speedCurve;




    private IEnumerator ShowScore() {
        int tempScore = 0; 
        _scoreText.text = tempScore.ToString();

        int currentScore = GameManager.Instance.CurrentScore;
        int highScore = GameManager.Instance.HighScore;

        if (currentScore > highScore) {
            _newBestText.gameObject.SetActive(true);
            GameManager.Instance.HighScore = currentScore;
        } else {
            _newBestText.gameObject.SetActive(false) ;
        }
        _highScoreText.text = GameManager.Instance.HighScore.ToString() ;

        float speed = 1 / _animationTime;
        float timeElasped = 0f;
        while (timeElasped < 1f) {
            timeElasped += speed * Time.deltaTime;
            tempScore = (int) (_speedCurve.Evaluate(timeElasped) * currentScore);
            _scoreText.text = tempScore.ToString() ;
            yield return null;
        }

        tempScore = currentScore;
        _scoreText.text = tempScore.ToString();

    }

    [SerializeField] private AudioClip _clickClip;

    public void ClickedPlay() {
        SoundManager.Instance.PlaySound(_clickClip);
        GameManager.Instance.GoToGamePlay();

    }

    public void QuitGame() {
        Application.Quit();

    }
}
