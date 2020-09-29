
export class microphoneToTab2 {

    langs =
        [['עברית', ['iw-IL']],
        ['Afrikaans', ['af-ZA']],
        ['Bahasa Indonesia', ['id-ID']],
        ['Bahasa Melayu', ['ms-MY']],
        ['Català', ['ca-ES']],
        ['Čeština', ['cs-CZ']],
        ['Deutsch', ['de-DE']],
        ['English', ['en-AU', 'Australia'],
            ['en-CA', 'Canada'],
            ['en-IN', 'India'],
            ['en-NZ', 'New Zealand'],
            ['en-ZA', 'South Africa'],
            ['en-GB', 'United Kingdom'],
            ['en-US', 'United States']],
        ['Español', ['es-AR', 'Argentina'],
            ['es-BO', 'Bolivia'],
            ['es-CL', 'Chile'],
            ['es-CO', 'Colombia'],
            ['es-CR', 'Costa Rica'],
            ['es-EC', 'Ecuador'],
            ['es-SV', 'El Salvador'],
            ['es-ES', 'España'],
            ['es-US', 'Estados Unidos'],
            ['es-GT', 'Guatemala'],
            ['es-HN', 'Honduras'],
            ['es-MX', 'México'],
            ['es-NI', 'Nicaragua'],
            ['es-PA', 'Panamá'],
            ['es-PY', 'Paraguay'],
            ['es-PE', 'Perú'],
            ['es-PR', 'Puerto Rico'],
            ['es-DO', 'República Dominicana'],
            ['es-UY', 'Uruguay'],
            ['es-VE', 'Venezuela']],
        ['Euskara', ['eu-ES']],
        ['Français', ['fr-FR']],
        ['Galego', ['gl-ES']],
        ['Hrvatski', ['hr_HR']],
        ['IsiZulu', ['zu-ZA']],
        ['Íslenska', ['is-IS']],
        ['Italiano', ['it-IT', 'Italia'],
            ['it-CH', 'Svizzera']],
        ['Magyar', ['hu-HU']],
        ['Nederlands', ['nl-NL']],
        ['Norsk bokmål', ['nb-NO']],
        ['Polski', ['pl-PL']],
        ['Português', ['pt-BR', 'Brasil'],
            ['pt-PT', 'Portugal']],
        ['Română', ['ro-RO']],
        ['Slovenčina', ['sk-SK']],
        ['Suomi', ['fi-FI']],
        ['Svenska', ['sv-SE']],
        ['Türkçe', ['tr-TR']],
        ['български', ['bg-BG']],
        ['Pусский', ['ru-RU']],
        ['Српски', ['sr-RS']],
        ['한국어', ['ko-KR']],
        ['中文', ['cmn-Hans-CN', '普通话 (中国大陆)'],
            ['cmn-Hans-HK', '普通话 (香港)'],
            ['cmn-Hant-TW', '中文 (台灣)'],
            ['yue-Hant-HK', '粵語 (香港)']],
        ['日本語', ['ja-JP']],
        ['Lingua latīna', ['la']]];
    messages = {
        "start": {
            msg: 'Click on the microphone icon and begin speaking.',
            class: 'alert-success'
        },
        "speak_now": {
            msg: 'Speak now.',
            class: 'alert-success'
        },
        "no_speech": {
            msg: 'No speech was detected. You may need to adjust your <a href="//support.google.com/chrome/answer/2693767" target="_blank">microphone settings</a>.',
            class: 'alert-danger'
        },
        "no_microphone": {
            msg: 'No microphone was found. Ensure that a microphone is installed and that <a href="//support.google.com/chrome/answer/2693767" target="_blank">microphone settings</a> are configured correctly.',
            class: 'alert-danger'
        },
        "allow": {
            msg: 'Click the "Allow" button above to enable your microphone.',
            class: 'alert-warning'
        },
        "denied": {
            msg: 'Permission to use microphone was denied.',
            class: 'alert-danger'
        },
        "blocked": {
            msg: 'Permission to use microphone is blocked. To change, go to chrome://settings/content/microphone',
            class: 'alert-danger'
        },
        "upgrade": {
            msg: 'Web Speech API is not supported by this browser. It is only supported by <a href="//www.google.com/chrome">Chrome</a> version 25 or later on desktop and Android mobile.',
            class: 'alert-danger'
        },
        "stop": {
            msg: 'Stop listening, click on the microphone icon to restart',
            class: 'alert-success'
        },
        "copy": {
            msg: 'Content copy to clipboard successfully.',
            class: 'alert-success'
        },
    }
    final_transcript = '';
    recognizing = false;
    ignore_onend;
    start_timestamp;
    recognition;

    select_dialect;
    select_language;
    start_button;
    languageOptions = [];
    initializeApp() {

        // for (var i = 0; i < this.langs.length; i++) {
        //     this.languageOptions.push(this.langs[0][i][0])
        //     console.log(this.langs[0][i][0])
        // }
        this.select_language.selectedIndex = 0;
        // updateCountry();
        // this.select_dialect.selectedIndex = 0;

        if (!('webkitSpeechRecognition' in window)) {
            upgrade();
        } else {
            showInfo('start');
            this.start_button.style.display = 'inline-block';
            recognition = new webkitSpeechRecognition();
            recognition.continuous = true;
            recognition.interimResults = true;

            recognition.onstart = function () {
                recognizing = true;
                showInfo('speak_now');
                start_img.src = '/../../assets/images/mic-animation.gif';
            };

            recognition.onerror = function (event) {
                if (event.error == 'no-speech') {
                    start_img.src = '/../../assets/images/mic.gif';
                    showInfo('no_speech');
                    ignore_onend = true;
                }
                if (event.error == 'audio-capture') {
                    start_img.src = '/../../assets/images/mic.gif';
                    showInfo('no_microphone');
                    ignore_onend = true;
                }
                if (event.error == 'not-allowed') {
                    if (event.timeStamp - start_timestamp < 100) {
                        showInfo('blocked');
                    } else {
                        showInfo('denied');
                    }
                    ignore_onend = true;
                }
            };

            recognition.onend = function () {
                recognizing = false;
                if (ignore_onend) {
                    return;
                }
                start_img.src = '../../assets/images/mic.gif';
                if (!final_transcript) {
                    showInfo('start');
                    return;
                }
                showInfo('stop');
                if (window.getSelection) {
                    window.getSelection().removeAllRanges();
                    var range = document.createRange();
                    range.selectNode(document.getElementById('final_span'));
                    window.getSelection().addRange(range);
                }
            };

            recognition.onresult = function (event) {
                var interim_transcript = '';
                for (var i = event.resultIndex; i < event.results.length; ++i) {
                    if (event.results[i].isFinal) {
                        final_transcript += event.results[i][0].transcript;
                    } else {
                        interim_transcript += event.results[i][0].transcript;
                    }
                }
                final_transcript = capitalize(final_transcript);
                final_span.innerHTML = linebreak(final_transcript);
                interim_span.innerHTML = linebreak(interim_transcript);
            };
        }
    }


    // updateCountry() {
    //     for (var i = select_dialect.options.length - 1; i >= 0; i--) {
    //         select_dialect.remove(i);
    //     }
    //     var list = langs[select_language.selectedIndex];
    //     for (var i = 1; i < list.length; i++) {
    //         select_dialect.options.add(new Option(list[i][1], list[i][0]));
    //     }
    //     select_dialect.style.visibility = list[1].length == 1 ? 'hidden' : 'visible';
    // }


 upgrade() {
    start_button.style.visibility = 'hidden';
    showInfo('upgrade');
}

 two_line = /\n\n/g;
 one_line = /\n/g;
 linebreak(s) {
    return s.replace(two_line, '<p></p>').replace(one_line, '<br>');
}

 first_char = /\S/;
 capitalize(s) {
    return s.replace(first_char, function (m) { return m.toUpperCase(); });
}

$("#copy_button").click(function () {
    if (recognizing) {
        recognizing = false;
        recognition.stop();
    }
    setTimeout(copyToClipboard, 500);

});

 copyToClipboard() {
    if (document.selection) {
        var range = document.body.createTextRange();
        range.moveToElementText(document.getElementById('results'));
        range.select().createTextRange();
        document.execCommand("copy");

    } else if (window.getSelection) {
        var range = document.createRange();
        range.selectNode(document.getElementById('results'));
        window.getSelection().addRange(range);
        document.execCommand("copy");
    }
    showInfo('copy');
}

$("#start_button").click(function () {
    if (recognizing) {
        recognition.stop();
        return;
    }
    final_transcript = '';
    recognition.lang = select_dialect.value;
    recognition.start();
    ignore_onend = false;
    final_span.innerHTML = '';
    interim_span.innerHTML = '';
    start_img.src = 'images/mic-slash.gif';
    showInfo('allow');
    start_timestamp = event.timeStamp;
});

$("#select_language").change(function () {
    updateCountry();
});

 showInfo(s) {
    if (s) {
        var message = messages[s];
        $("#info").html(message.msg);
        $("#info").removeClass();
        $("#info").addClass('alert');
        $("#info").addClass(message.class);
    } else {
        $("#info").removeClass();
        $("#info").addClass('d-none');
    }
}
