var instance = false;
var state;
var file;

function Chat()
{
    this.update = updateChat;
    this.send = sendChat;
    this.getState = getStateOfChat;
}

function getStateOfChat()
{
    if(!instance)
    {
        instance = true;
        $.ajax({
            type: "POST",
            url: "ChatProcess.php",
            data: {
                'function': 'getState',
                'file': file
                    },
            dataType: "json",
            success: function(data)
            {
                state = data.state;
                instance = false;
            }
        })
    }
}

function updateChat()
{
    if(!instance)
    {
        instance = true;
        $.ajax({
            type: "POST",
            url: "ChatProcess.php",
            data: {'function':'update','state':state,'file':file},
            dataType: "json",
            success: function(data)
            {
                if(data.text)
                {
                    for(var i = 0; i < data.text.length; i++)
                    {
                        $('#chatArea').append($(""+ data.text[i] +""));
                    }
                }
                document.getElementById('chatArea').scrollTop = document.getElementById('chatArea').scrollHeight;
                instance = false;
                state = data.state;
            }
        });
    }
    else
    {
        setTimeout(updateChat, 1500);
    }
}

function sendChat(message, nickname)
{
    updateChat();
    $.ajax({
        type: "POST",
        url: "ChaTProcess.php",
        data: {'function': 'send','message': message, 'nickname': nickname, 'file':file},
        dataType: "json",
        success: function(data)
        {
            updateChat();
        }
    })
}