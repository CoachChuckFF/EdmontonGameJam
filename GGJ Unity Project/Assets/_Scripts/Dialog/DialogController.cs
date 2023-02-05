using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DialogController : MonoBehaviour//, IPointerDownHandler
{
    [SerializeField]
    private TMP_Text m_NameText;

    [SerializeField]
    private TMP_Text m_TextBox;

    private TextWriter _currentTextWriter;

    public bool IsStarted { get; private set; } = false;

    public bool IsComplete => _currentTextWriter != null ? _currentTextWriter.IsComplete : false;

    public UnityEvent OnContinue;

    //void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    //{
    //    if (_currentTextWriter != null)
    //    {
    //        if (_currentTextWriter.IsComplete)
    //        {
    //            OnContinue?.Invoke();

    //        }
    //        else
    //        {
    //            _currentTextWriter.Skip();
    //            StopAllCoroutines();
    //        }
    //    }
    //}

    public void SetDialog(ConversationNode conversation)
    {
        m_NameText.text = conversation.Character.ToString();
        _currentTextWriter = new TextWriter(conversation.Text);
    }

    public void StartDialog()
    {
        if (_currentTextWriter != null)
        {
            StartCoroutine(_currentTextWriter.GetTextEnumerator(m_TextBox));
            IsStarted = true;
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && _currentTextWriter != null && IsStarted)
        {
            if (_currentTextWriter.IsComplete)
            {
                OnContinue?.Invoke();
            }
            else
            {
                _currentTextWriter.Skip();
                StopAllCoroutines();
            }
        }
    }

}
