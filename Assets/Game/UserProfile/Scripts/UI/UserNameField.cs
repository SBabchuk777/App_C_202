using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace UserProfile.UI
{
    public class UserNameField : MonoBehaviour
    {
        [SerializeField] private GameObject _normalState = null;
        [SerializeField] private GameObject _editingState = null;
    
        [Space]
    
        [SerializeField] private Text _nameText = null;

        [SerializeField] private InputField _editNameField = null;

        private void Awake()
        {
            _normalState.SetActive(true);
            _editingState.SetActive(false);
        
            _editNameField.onEndEdit.AddListener(SaveNewName);
            _editNameField.onValueChanged.AddListener(ValidateInput);

            UserProfileStorage.OnChangedUserName += UpdateNameText;

            UpdateNameText(UserProfileStorage.UserName);
        }
        
        private void ValidateInput(string text)
        {
            int startCount = text.Length;
            
            text = Regex.Replace(text, "[^a-zA-Z0-9]", "");

            int offset = startCount - text.Length;
            
            _editNameField.text = text;
            _editNameField.caretPosition -= offset;
        }

        private void OnDestroy()
        {
            UserProfileStorage.OnChangedUserName -= UpdateNameText;
        }

        private void SaveNewName(string name)
        {
            _normalState.SetActive(true);
            _editingState.SetActive(false);

            UserProfileStorage.UserName = name;
        }

        private void UpdateNameText(string name)
        {
            _nameText.text = name;
        }

        public void StartEditing()
        {
            _normalState.SetActive(false);
            _editingState.SetActive(true);

            _editNameField.text = UserProfileStorage.UserName;
        
            _editNameField.ActivateInputField();
        }
    }
}
