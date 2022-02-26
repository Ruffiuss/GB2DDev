using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FightWindowView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _countMoneyText;

    [SerializeField]
    private TMP_Text _countHealthText;

    [SerializeField]
    private TMP_Text _countPowerText;
    
    [SerializeField]
    private TMP_Text _countWantedText;

    [SerializeField]
    private TMP_Text _countPowerEnemyText;


    [SerializeField]
    private Button _addMoneyButton;

    [SerializeField]
    private Button _minusMoneyButton;


    [SerializeField]
    private Button _addHealthButton;

    [SerializeField]
    private Button _minusHealthButton;


    [SerializeField]
    private Button _addPowerButton;

    [SerializeField]
    private Button _minusPowerButton;


    [SerializeField]
    private Button _addWantedButton;

    [SerializeField]
    private Button _minusWantedButton;


    [SerializeField]
    private Button _fightButton;

    [SerializeField]
    private Button _skipFightButton;

    private Enemy _enemy;

    private Money _money;
    private Health _health;
    private Power _power;
    private Wanted _wanted;

    private int _allCountMoneyPlayer;
    private int _allCountHealthPlayer;
    private int _allCountPowerPlayer;
    private int _allCountWantedPlayer;

    private void Start()
    {
        _enemy = new Enemy("Flappy");

        _money = new Money(nameof(Money));
        _money.Attach(_enemy);

        _health = new Health(nameof(Health));
        _health.Attach(_enemy);

        _power = new Power(nameof(Power));
        _power.Attach(_enemy);

        _wanted = new Wanted(nameof(Wanted));
        _wanted.Attach(_enemy);

        _addMoneyButton.onClick.AddListener(() => ChangeMoney(true));
        _minusMoneyButton.onClick.AddListener(() => ChangeMoney(false));

        _addHealthButton.onClick.AddListener(() => ChangeHealth(true));
        _minusHealthButton.onClick.AddListener(() => ChangeHealth(false));

        _addPowerButton.onClick.AddListener(() => ChangePower(true));
        _minusPowerButton.onClick.AddListener(() => ChangePower(false));

        _addWantedButton.onClick.AddListener(() => ChangeWanted(true));
        _minusWantedButton.onClick.AddListener(() => ChangeWanted(false));

        _fightButton.onClick.AddListener(Fight);
        _skipFightButton.onClick.AddListener(SkipFight);
    }

    private void OnDestroy()
    {
        _addMoneyButton.onClick.RemoveAllListeners();
        _minusMoneyButton.onClick.RemoveAllListeners();

        _addHealthButton.onClick.RemoveAllListeners();
        _minusHealthButton.onClick.RemoveAllListeners();

        _addPowerButton.onClick.RemoveAllListeners();
        _minusPowerButton.onClick.RemoveAllListeners();

        _addWantedButton.onClick.RemoveAllListeners();
        _minusWantedButton.onClick.RemoveAllListeners();

        _fightButton.onClick.RemoveAllListeners();
        _skipFightButton.onClick.RemoveAllListeners();

        _money.Detach(_enemy);
        _health.Detach(_enemy);
        _power.Detach(_enemy);
        _wanted.Detach(_enemy);
    }

    private void Fight()
    {
        Debug.Log(_allCountPowerPlayer >= _enemy.Power ? "Win" : "Lose");
    }

    private void SkipFight()
    {
        var result = _allCountPowerPlayer >= _enemy.Power ? "win" : "lose";
        Debug.Log($"Fight was skipped, you could have {result} it");
    }

    private void ChangePower(bool isAddCount)
    {
        if (isAddCount)
            _allCountPowerPlayer++;
        else
            _allCountPowerPlayer--;

        ChangeDataWindow(_allCountPowerPlayer, DataType.Power);
    }

    private void ChangeHealth(bool isAddCount)
    {
        if (isAddCount)
            _allCountHealthPlayer++;
        else
            _allCountHealthPlayer--;

        ChangeDataWindow(_allCountHealthPlayer, DataType.Health);
    }

    private void ChangeMoney(bool isAddCount)
    {
        if (isAddCount)
            _allCountMoneyPlayer++;
        else
            _allCountMoneyPlayer--;

        ChangeDataWindow(_allCountMoneyPlayer, DataType.Money);
    }

    private void ChangeWanted(bool isAddCount)
    {
        if (isAddCount)
            _allCountWantedPlayer++;
        else
            _allCountWantedPlayer--;

        ChangeDataWindow(_allCountWantedPlayer, DataType.Wanted);
    }

    private void ChangeDataWindow(int countChangeData, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Money:
                _countMoneyText.text = $"Player money: {countChangeData}";
                _money.CountMoney = countChangeData;
                break;

            case DataType.Health:
                _countHealthText.text = $"Player health: {countChangeData}";
                _health.CountHealth = countChangeData;
                break;

            case DataType.Power:
                _countPowerText.text = $"Player power: {countChangeData}";
                _power.CountPower = countChangeData;
                break;

            case DataType.Wanted:
                _countWantedText.text = $"Player wanted: {countChangeData}";
                _wanted.CountWanted = countChangeData;
                _skipFightButton.gameObject.SetActive(_wanted.CountWanted >= 3 ? false : true);
                break;
        }

        _countPowerEnemyText.text = $"Enemy power: {_enemy.Power}";
    }
}
