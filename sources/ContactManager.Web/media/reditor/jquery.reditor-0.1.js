 /*
* Reditor 0.8.2
*
*
* http://www.redoxite.ch
*/

(function($) {
    $.fn.reditor = function(options) {
        // SET OPTIONS / DEFAULTS
        var defaults = {
            sliderorientation: 'horizontal',
            basepath: 'reditor/',
            modalpopups: true,
            styleclasses: [],
            plugins: [],
            topmenu: true,
            bottominfo: true,
            htmltab: true,
            iconpath: 'css/img/ico/',
            browser: {
                target: '#reditor-browser',
                connector: 'connector.aspx',
                folder: ''
            },
            width: '500px',
            height: '500px',
            //save: function(Data) { alert(Data); },
            save: function(Data) { SubmitCustomResource(Data); },
            debug: false,
            fonts: [
                { value: 'times new roman', name: 'Times New Roman', type: 'font-family' },
                { value: 'georgia', name: 'Georgia', type: 'font-family' },
                { value: 'andale mono', name: 'Andale Mono', type: 'font-family' },
                { value: 'arial', name: 'Arial', type: 'font-family' },
                { value: 'arial black', name: 'Arial Black', type: 'font-family' },
                { value: 'century gothic', name: 'Century Gothic', type: 'font-family' },
                { value: 'impact', name: 'Impact', type: 'font-family' },
                { value: 'trebuchet ms', name: 'Trebuchet MS', type: 'font-family' },
                { value: 'verdana', name: 'Verdana', type: 'font-family' },
                { value: 'comic sans ms', name: 'Comic Sans MS', type: 'font-family' },
                { value: 'courier new', name: 'Courier New', type: 'font-family' }
            ]
        };

        var options = $.extend(defaults, options);

        // ! debug !
        if (options.debug) {
            var logLine = 0;
            if (options.debug)
                $('body').append('<div id="reditor-dug-console" class="reditor-debug-console"><table></table></div>');
            function Log(message, color) {
                if (color == null)
                    color = '#000000';
                $('#reditor-debug-console table').prepend('<tr><td style="color:#0000ff;vertical-align:top">' + (logLine++) +
                    ':</td><td style="color:' + color + '">' + message + '</td></tr>');
            }
        }
        // ! end debug !

        // VARIABLES
        var current = {
            instance: '',
            element: '',
            html: '',
            elementstack: '',
            selection: ''
        };
        var lastUsedClass;
        var lastUsedFont;

        // MENU BUILDER

        var menu = function(container) {
            var newMenu = container;
            this.addItem = function(item) {
                if (item.visible == true) {
                    if (item.type == 'button') {
                        var button = createButton(item.name, options.iconpath + item.icon);
                        $(button).children('button').click(function() {
                            item.execute($(this));
                        });
                        $(newMenu).append(button);
                    }
                    if (item.type == 'dropdown') {
                        var dropdown = createDropDown(item.name, options.iconpath + item.icon, item.values);
                        var list = $(dropdown).children('.reditor-dropdown-container');

                        $(dropdown).children('.reditor-remenu-icon-split').click(function() {
                            item.execute[0]();
                        });

                        $(dropdown).children('.reditor-remenu-select').click(function() {
                            list.toggle();
                            list.css('left', $(this).position().left - 25);
                            list.css('top', $(this).position().top + 25);
                            list.mouseleave(function() {
                                $(this).fadeOut('normal');
                            });
                            current.selection = saveSelection();
                        });

                        list.children('div').children('.reditor-dropdown-item').click(function() {
                            item.execute[1]($(this).attr('title'));
                        });

                        $(newMenu).append(dropdown);
                    }
                    if (item.type == 'slider') {
                        var slider = createSlider(item.name, options.iconpath + item.icon);
                        $(slider).children('button').click(function() {
                            item.execute($(slider));
                        });

                        $(slider).children('div').mouseleave(function() {
                            $(this).fadeOut();
                        });

                        $(newMenu).append(slider);
                    }
                    if (item.type == 'separator') {
                        $(newMenu).append(createSeparator());
                    }
                    if (item.type == 'linebreak') {
                        $(newMenu).append('<br />');
                    }
                }
            };
            this.html = function() {
                return newMenu;
            }
        };

        function createButton(action, icon) {
            var button = document.createElement('button');
            var img = document.createElement('img');

            $(button).attr('class', 'reditor-remenu-icon');

            $(img).attr('src', icon);
            $(img).attr('alt', action);
            button.appendChild(img);

            var span = document.createElement('span');
            span.appendChild(button);

            return span;
        }

        function createSeparator() {
            var span = document.createElement('span');
            var img = document.createElement('img');

            $(span).attr('class', 'reditor-separator');

            $(img).attr('src', options.basepath + 'css/img/separator.png');
            $(img).attr('alt', '|');
            span.appendChild(img);

            return span;
        }

        function createDropDown(action, icon, values) {
            var button1 = document.createElement('button');
            var button2 = document.createElement('button');

            var img1 = document.createElement('img');
            var img2 = document.createElement('img');

            var div = document.createElement('div');
            var container = document.createElement('div');

            $(button1).addClass('reditor-remenu-icon-split');
            $(button2).addClass('reditor-remenu-select');

            $(div).addClass('reditor-dropdown-container');
            $(container).addClass('reditor-dropdown');
            $(container).addClass('ui-widget-content');
            $(container).addClass('ui-corner-all');
            $(container).css('border', '2px solid #999999');

            $(img1).attr('src', icon);
            $(img1).attr('alt', action);

            $(img2).attr('src', options.basepath + 'css/img/drop_down.png');
            $(img2).attr('alt', action);

            $(button1).append(img1);
            $(button2).append(img2);

            for (var i = 0; i < values.length; i++) {
                var element = document.createElement('div');
                $(element).attr('title', values[i].value);
                $(element).addClass('reditor-dropdown-item');
                if (values[i].type == 'class') {
                    $(element).addClass(values[i].value);
                }
                else {
                    $(element).css(values[i].type, values[i].value);
                }
                $(element).html(values[i].name);
                $(container).append(element);
            }

            $(div).append(container);

            var span = document.createElement('span');
            $(span).append(button1);
            $(span).append(button2);
            $(span).append(div);
            return span;
        }

        function createSlider(action, icon) {
            var container = document.createElement('div');
            var slider = document.createElement('div');
            var value = document.createElement('div');

            $(slider).addClass('reditor-slider');
            $(value).addClass('reditor-slider-value');
            $(value).addClass('ui-widget-content');
            $(value).addClass('ui-corner-all');
            $(value).html('0em');
            $(container).append(slider);
            $(container).append(value);
            $(container).addClass('ui-corner-all');
            $(container).addClass('ui-widget-content');
            $(container).addClass('reditor-slider-container');

            var button = document.createElement('button');
            var img = document.createElement('img');

            $(button).attr('class', 'reditor-remenu-icon');

            $(img).attr('src', icon);
            $(img).attr('alt', action);
            button.appendChild(img);

            var span = document.createElement('span');
            $(span).append(button);
            $(span).append(container);
            return span;
        }

        // FORMATTING FUNCTIONS
        var reditorBold = function(button) {
            document.execCommand('bold', null, false);
        };

        var reditorItalic = function(button) {
            document.execCommand('italic', null, false);
        };

        var reditorUnderline = function(button) {
            document.execCommand('underline', null, false);
        };

        var reditorStrikethrough = function(button) {
            document.execCommand('strikethrough', null, false);
        };

        var reditorSubscript = function(button) {
            document.execCommand('subscript', null, false);
        };

        var reditorSuperscript = function(button) {
            document.execCommand('superscript', null, false);
        };

        var reditorQuote = function(button) {
            if ($.browser.msie) {
                var selection = document.selection;
                var range = selection.createRange();

                range.pasteHTML('<blockquote>' + range.text + '</blockquote>');
            }
            else {
                var range = window.getSelection().getRangeAt(0);
                var newNode = document.createElement("blockquote");
                range.surroundContents(newNode);
            }
        };

        var reditorAlignLeft = function(button) {
            current.element.css('text-align', 'left');
        };

        var reditorAlignRight = function(button) {
            current.element.css('text-align', 'right');
        };

        var reditorAlignCenter = function(button) {
            current.element.css('text-align', 'center');
        };

        var reditorAlignJustify = function(button) {
            current.element.css('text-align', 'justify');
        };

        var reditorClass = function(button) {
            current.element.css('style', '');
            current.element.addClass(lastUsedClass);
        };

        var reditorClassSelect = function(value) {
            current.element.removeClass();
            current.element.css('style', '');
            current.element.addClass(value);
            lastUsedClass = value;
        };

        var reditorFontSelect = function(value) {
            current.element.css('font-family', value);
            lastUsedFont = value;
        };

        var reditorFont = function() {
            current.element.css('font-family', lastUsedFont);
        };

        var reditorOrderedList = function(button) {
            document.execCommand('insertOrderedList', null, false);
        };

        var reditorUnorderedList = function(button) {
            document.execCommand('insertUnorderedList', null, false);
        };

        var reditorHorizontalRule = function(button) {
            var hr = document.createElement('hr');
            current.element.after(hr);
        };

        var reditorAllCaps = function(button) {
            current.element.css('font-variant', 'small-caps');
        };

        var reditorNoCaps = function(button) {
            current.element.css('font-variant', 'normal');
        };

        var reditorRemoveClass = function(button) {
            current.element.removeClass();
        };

        var reditorSave = function(button) {
            options.save(current.instance.html());
        };

        var reditorPaddingTop = function(container) {
            var button = container.children('button');
            var div = container.children('div');
            div.css('left', button.offset().left);
            div.css('top', button.offset().top + button.outerHeight());
            div.toggle();

            div.children('.reditor-slider-value').html(current.element.css('padding-top'));
            div.children('.reditor-slider').slider({
                orientation: options.sliderorientation,
                value: 0,
                min: 0,
                max: 100,
                step: 1,
                slide: function(event, ui) {
                    if (ui.value == 0) {
                        current.element.css('padding-top', '0px');
                        div.children('.reditor-slider-value').html('0px');
                        return;
                    } else {
                        current.element.css('padding-top', ui.value + 'px');
                        div.children('.reditor-slider-value').html(ui.value + 'px');
                    }
                }
            });
        };

        var reditorPaddingLeft = function(container) {
            var button = container.children('button');
            var div = container.children('div');
            div.css('left', button.offset().left);
            div.css('top', button.offset().top + button.outerHeight());
            div.toggle();

            div.children('.reditor-slider-value').html(current.element.css('padding-left'));
            div.children('.reditor-slider').slider({
                orientation: options.sliderorientation,
                value: 0,
                min: 0,
                max: 100,
                step: 1,
                slide: function(event, ui) {
                    if (ui.value == 0) {
                        current.element.css('padding-left', '0px');
                        div.children('.reditor-slider-value').html('0px');
                        return;
                    } else {
                        current.element.css('padding-left', ui.value + 'px');
                        div.children('.reditor-slider-value').html(ui.value + 'px');
                    }
                }
            });
        };

        var reditorPaddingBottom = function(container) {
            var button = container.children('button');
            var div = container.children('div');
            div.css('left', button.offset().left);
            div.css('top', button.offset().top + button.outerHeight());
            div.toggle();

            div.children('.reditor-slider-value').html(current.element.css('padding-bottom'));
            div.children('.reditor-slider').slider({
                orientation: options.sliderorientation,
                value: 0,
                min: 0,
                max: 100,
                step: 1,
                slide: function(event, ui) {
                    if (ui.value == 0) {
                        current.element.css('padding-bottom', '0px');
                        div.children('.reditor-slider-value').html('0px');
                        return;
                    } else {
                        current.element.css('padding-bottom', ui.value + 'px');
                        div.children('.reditor-slider-value').html(ui.value + 'px');
                    }
                }
            });
        };

        var reditorPaddingRight = function(container) {
            var button = container.children('button');
            var div = container.children('div');
            div.css('left', button.offset().left);
            div.css('top', button.offset().top + button.outerHeight());
            div.toggle();

            div.children('.reditor-slider-value').html(current.element.css('padding-right'));
            div.children('.reditor-slider').slider({
                orientation: options.sliderorientation,            
                value: 0,
                min: 0,
                max: 100,
                step: 1,
                slide: function(event, ui) {
                    if (ui.value == 0) {
                        current.element.css('padding-right', '0px');
                        div.children('.reditor-slider-value').html('0px');
                        return;
                    } else {
                        current.element.css('padding-right', ui.value + 'px');
                        div.children('.reditor-slider-value').html(ui.value + 'px');
                    }
                }
            });
        };

        var reditorFontSize = function(container) {
            var button = container.children('button');
            var div = container.children('div');
            div.css('left', button.offset().left);
            div.css('top', button.offset().top + button.outerHeight());
            div.toggle();

            div.children('.reditor-slider-value').html(current.element.css('font-size'));
            div.children('.reditor-slider').slider({
                orientation: options.sliderorientation,                
                value: 0,
                min: 0,
                max: 5,
                step: 0.1,
                slide: function(event, ui) {
                    if (ui.value == 0) {
                        current.element.css('font-size', 'normal');
                        div.children('.reditor-slider-value').html('normal');
                    } else {
                        current.element.css('font-size', ui.value + 'em');
                        div.children('.reditor-slider-value').html(ui.value + 'em');
                    }
                }
            });
        };

        var reditorLetterSpacing = function(container) {
            var button = container.children('button');
            var div = container.children('div');
            div.css('left', button.offset().left);
            div.css('top', button.offset().top + button.outerHeight());
            div.toggle();

            div.children('.reditor-slider-value').html(current.element.css('letter-spacing'));
            div.children('.reditor-slider').slider({
                orientation: options.sliderorientation,
                value: 0,
                min: 0,
                max: 5,
                step: 0.1,
                slide: function(event, ui) {
                    if (ui.value == 0) {
                        current.element.css('letter-spacing', 'normal');
                        div.children('.reditor-slider-value').html('normal');
                    } else {
                        current.element.css('letter-spacing', ui.value + 'em');
                        div.children('.reditor-slider-value').html(ui.value + 'em');
                    }
                }
            });
        };

        var reditorLineSpacing = function(container) {
            var button = container.children('button');
            var div = container.children('div');
            div.css('left', button.offset().left);
            div.css('top', button.offset().top + button.outerHeight());
            div.toggle();

            div.children('.reditor-slider-value').html(current.element.css('line-height'));
            div.children('.reditor-slider').slider({
                orientation: options.sliderorientation,
                value: 0,
                min: 0,
                max: 5,
                step: 0.1,
                slide: function(event, ui) {
                    if (ui.value == 0) {
                        current.element.css('line-height', 'normal');
                        div.children('.reditor-slider-value').html('normal');
                        return;
                    } else {
                        current.element.css('line-height', ui.value + 'em');
                        div.children('.reditor-slider-value').html(ui.value + 'em');
                    }
                }
            });
        };

        var reditorTextColour = function(button) {
            $(button).colourpicker({
                ok: function(data) {
                    current.element.css('color', data);
                }
            });
        };

        var reditorBackgroundColour = function(button) {
            $(button).colourpicker({
                ok: function(data) {
                    current.element.css('background-color', data);
                }
            });
        };

        var reditorLink = function(button) {
            current.selection = saveSelection();
            var dialog = document.createElement('div');
            dialog.setAttribute('title', 'Insert a Link');
            dialog.appendChild(createTextBox('remenu-action-link-text', 'Input an url', 'For example http://www.redoxite.ch.'));
            $(dialog).dialog({
                dialogClass: 'reditor-dialog',
                bgiframe: true,
                modal: options.modalpopups,
                buttons: {
                    Ok: function() {
                        loadSelection(current.selection);
                        document.execCommand('createLink', false, $('#remenu-action-link-text').val());
                        $(this).dialog('close');
                        $(this).remove();
                    },
                    Cancel: function() {
                        $(this).dialog('close');
                        $(this).remove();
                    }
                }
            });
        };

        var reditorUnlink = function(button) {
            if (current.element[0].tagName.toLowerCase() == 'a') {
                current.element.after(current.html);
                current.element.remove();
            }
        };

        var reditorTable = function(button) {
            current.selection = saveSelection();
            var dialog = document.createElement('div');
            dialog.appendChild(createTextBox('remenu-action-table-rows', i18n.tableDialog.b, i18n.tableDialog.c));
            dialog.appendChild(createTextBox('remenu-action-table-cols', i18n.tableDialog.d, i18n.tableDialog.e));
            dialog.appendChild(createTextBox('remenu-action-table-width', i18n.tableDialog.f, i18n.tableDialog.g));
            dialog.appendChild(createCheckBox('remenu-action-table-header', i18n.tableDialog.h, i18n.tableDialog.i));
            dialog.setAttribute('title', i18n.tableDialog.a);
            $(dialog).dialog({
                dialogClass: 'reditor-dialog',
                bgiframe: true,
                modal: options.modalpopups,
                buttons: {
                    Ok: function() {
                        var html = '<p><table class="reditor-content-table" style="width:' +
                            $('#remenu-action-table-width').val() + '">';

                        if ($('#remenu-action-table-header').attr('checked')) {
                            html += '<thead><tr>';
                            for (i = 0; i < $('#remenu-action-table-rows').val(); i++)
                                html += '<th>Header ' + i + '</th>';
                            html += '</tr></thead>';
                        }

                        for (i = 0; i < $('#remenu-action-table-rows').val(); i++) {
                            html += '<tr>';
                            for (k = 0; k < $('#remenu-action-table-cols').val(); k++) {
                                html += '<td>';
                                html += 'Cell ' + k + '/' + i;
                                html += '</td>';
                            }
                            html += '</tr>';
                        }
                        html += '</table></p>';
                        current.element.after(html);
                        $(this).dialog('close');
                        $(this).remove();
                    },
                    Cancel: function() {
                        $(this).dialog('close');
                        $(this).remove();
                    }
                }
            });
        };

        var reditorEditCss = function(button) {
            current.selection = saveSelection();
            var dialog = document.createElement('div');
            var textArea = createTextArea(i18n.cssDialog.b, i18n.cssDialog.c, i18n.cssDialog.d);
            $(textArea).children('textarea').val(current.element.attr('style'));
            $(textArea).children('textarea').css('width', '500px');
            $(textArea).children('textarea').css('height', '200px');
            dialog.appendChild(textArea);
            dialog.setAttribute('title', i18n.cssDialog.a);
            $(dialog).dialog({
                dialogClass: 'reditor-dialog',
                bgiframe: true,
                width: 540,
                modal: options.modalpopups,
                buttons: {
                    Ok: function() {
                        loadSelection(current.selection);
                        var t = false;
                        if ($.browser.msie) {
                            var selection = document.selection;
                            var range = selection.createRange();
                            if (range.text.length == 0)
                                t = true;
                            else {
                                range.pasteHTML('<span style="' + $('#remenu-action-surroundcss-text').val() +
                                    '">' + range.text + '</span>');
                            }
                        }
                        else {
                            var range = window.getSelection().getRangeAt(0);
                            var newNode = document.createElement("span");
                            newNode.setAttribute('style', $('#remenu-action-surroundcss-text').val());
                            if (range.toString().length == 0) {
                                t = true;
                            } else {
                                range.surroundContents(newNode);
                            }
                        }

                        if (t) {
                            current.element.attr('style', $('#remenu-action-surroundcss-text').val());
                            t = false;
                        }

                        $(this).dialog('close');
                        $(this).remove();
                    },
                    Cancel: function() {
                        $(this).dialog('close');
                        $(this).remove();
                    }
                }
            });
        };

        var reditorImage = function(button) {
            current.selection = saveSelection();
            var dialog = document.createElement('div');
            var browser = document.createElement('div');
            var img = document.createElement('img');
            var column1 = createColumn();
            var column2 = createColumn();
            var column3 = createColumn();

            browser.setAttribute('id', 'reditor-browser');
            dialog.appendChild(browser);

            column1.appendChild(createTextBox('remenu-action-image-path', 'Path', i18n.imageDialog.b));

            column2.appendChild(createTextBox('remenu-action-image-width', 'Width', i18n.imageDialog.c));
            column2.appendChild(createTextBox('remenu-action-image-height', 'Height', i18n.imageDialog.d));

            column3.appendChild(createTextBox('remenu-action-image-border-width', 'Border Width', i18n.imageDialog.e));
            column3.appendChild(createTextBox('remenu-action-image-border-colour', 'Border Colour', i18n.imageDialog.f));

            dialog.appendChild(column1);
            dialog.appendChild(column2);
            dialog.appendChild(column3);
            dialog.setAttribute('title', i18n.imageDialog.a);
            openFolder(options.browser.folder, options.browser.connector, options.browser.target);
            $(dialog).dialog({
                width: 700,
                height: 530,
                resizable: false,
                bgiframe: true,
                modal: options.modalpopups,
                buttons: {
                    Ok: function() {
                        img.setAttribute('src', $('#remenu-action-image-path').val());
                        $(img).attr('style', 'border:' + $('#remenu-action-image-border-width').val() + 'px solid ' +
                            $('#remenu-action-image-border-colour').val() + ';width:' + $('#remenu-action-image-width').val() +
                            'px;height:' + $('#remenu-action-image-height').val() + 'px;');

                        inject(current.element, img);
                        $(this).dialog('close');
                        $(this).remove();
                    },
                    Cancel: function() {
                        $(this).dialog('close');
                        $(this).remove();
                    }
                }
            });
        };

        var reditorBorder = function(button) {
            current.selection = saveSelection();
            var dialog = document.createElement('div');
            dialog.appendChild(createTextBox('remenu-action-border-width', 'Width', 'Specify the desired with of the border in px.'));
            dialog.setAttribute('title', i18n.cssDialog.a);
            $(dialog).dialog({
                dialogClass: 'reditor-dialog',
                bgiframe: true,
                modal: options.modalpopups,
                buttons: {
                    Ok: function() {
                        loadSelection(current.selection);

                        current.element.css('border', $('#remenu-action-border-width').val() + 'px solid black');
                        current.element.addClass('ui-corner-all');

                        $(this).dialog('close');
                        $(this).remove();
                    },
                    Cancel: function() {
                        $(this).dialog('close');
                        $(this).remove();
                    }
                }
            });
        };


        var reditorPreview = function(obj) {
            $('.reditor-marker').remove();
            $('.reditor *').removeClass('reditor-current-element');
            html = '<html><head><title>Reditor Preview</title>';
            html += '<link rel="stylesheet" type="text/css" href="css/style.css" /><link rel="stylesheet" type="text/css" href="css/humanity/jquery-ui-1.7.2.custom.css" /><script type="text/javascript" src="jquery-1.3.2.min.js"></script><script type="text/javascript" src="jquery-ui-1.7.2.js"></script><script type="text/javascript" src="reditor-0.1.jquery.js"></script>';
            html += '<script type="text/javascript">$(document).ready(function(){$("#tabs").tabs();$("#accordion").accordion();});</script>';
            html += '</head><body>';
            html += current.instance.html();
            html = html.replace(/contentEditable=true/g, "");
            html = html.replace(/contenteditable=true/g, "");
            html = html.replace(/contenteditable="true"/g, "");
            html += '</body></html>';
            windowprops = 'left=50,top=50,width=500,height=500,resizable=yes';
            preview = window.open("", "preview", windowprops);
            preview.document.open();
            preview.document.write(html);
            preview.document.close();
        };

        /* ==========================================================================================
        PLUGIN LOGIC
        ==========================================================================================*/
        return this.each(function() {
            var obj = $(this);
            if (obj[0].tagName.toLowerCase() != 'div') {
                var reditor = document.createElement('div');
                $(reditor).addClass('reditor');
                $(reditor).html(obj.html());
                obj.after(reditor);
                obj.remove();
                obj = $(reditor);
            }
            var currentView = 'wysiwyg';

            if ($.browser.msie && obj.children().size() == 0)
                obj.append('<p></p>');

            obj.keyup(function() {
                if ($(this).children().size() == 0 && currentView == 'wysiwyg') {
                    var p = document.createElement('p');
                    $(p).text($(this).text());
                    $(this).text('');
                    $(this).append(p);
                    focusElement(p);
                }
            });

            obj.focus(function() {
                if (current.instance != obj) {
                    current.instance = obj;
                    // ! debug !
                    if (options.debug)
                        Log('Current instance changed.');
                    // ! end debug !
                }
            });

            obj.css('width', options.width);
            obj.css('height', options.height);

            var container = document.createElement('div');
            $(container).attr('class', 'reditor-container ui-corner-all');

            current.element = $(this).children('p');
            obj.attr('contentEditable', 'true');
            obj.addClass('reditor-element');

            // INITIALIZE REDITOR
            obj.wrap(container);

            // INITIALIZE TOPMENU
            options.iconpath = options.basepath + options.iconpath;
            var retop = document.createElement('div');
            $(retop).attr('class', 'ui-corner-top reditor-retop ui-widget-content');

            var topmenu = new menu(retop);
            topmenu.addItem({ name: 'bold', type: 'button', execute: reditorBold, icon: 'text_bold.png', visible: true });
            topmenu.addItem({ name: 'italic', type: 'button', execute: reditorItalic, icon: 'text_italic.png', visible: true });
            topmenu.addItem({ name: 'underline', type: 'button', execute: reditorUnderline, icon: 'text_underline.png', visible: true });
            topmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            topmenu.addItem({ name: 'strikethrough', type: 'button', execute: reditorStrikethrough, icon: 'text_strikethrough.png', visible: true });
            topmenu.addItem({ name: 'subscript', type: 'button', execute: reditorSubscript, icon: 'text_subscript.png', visible: true });
            topmenu.addItem({ name: 'superscript', type: 'button', execute: reditorSuperscript, icon: 'text_superscript.png', visible: true });
            topmenu.addItem({ name: 'quote', type: 'button', execute: reditorQuote, icon: 'comment.png', visible: true });
            topmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            topmenu.addItem({ name: 'align-left', type: 'button', execute: reditorAlignLeft, icon: 'text_align_left.png', visible: true });
            topmenu.addItem({ name: 'align-center', type: 'button', execute: reditorAlignCenter, icon: 'text_align_center.png', visible: true });
            topmenu.addItem({ name: 'align-right', type: 'button', execute: reditorAlignRight, icon: 'text_align_right.png', visible: true });
            topmenu.addItem({ name: 'align-justify', type: 'button', execute: reditorAlignJustify, icon: 'text_align_justify.png', visible: true });
            topmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            topmenu.addItem({ name: 'font', type: 'dropdown', execute: [reditorFont, reditorFontSelect], icon: 'font.png', values: options.fonts, visible: true });
            topmenu.addItem({ name: 'class', type: 'dropdown', execute: [reditorClass, reditorClassSelect], icon: 'style.png', values: options.styleclasses, visible: true });
            topmenu.addItem({ name: 'remove-class', type: 'button', execute: reditorRemoveClass, icon: 'style_delete.png', visible: true });
            topmenu.addItem({ name: 'fontsize', type: 'slider', execute: reditorFontSize, icon: 'font_size.png', visible: true });
            topmenu.addItem({ name: 'letterspacing', type: 'slider', execute: reditorLetterSpacing, icon: 'text_letterspacing.png', visible: true });
            topmenu.addItem({ name: 'linespacing', type: 'slider', execute: reditorLineSpacing, icon: 'text_linespacing.png', visible: true });
            topmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            topmenu.addItem({ name: 'allcaps', type: 'button', execute: reditorAllCaps, icon: 'text_allcaps.png', visible: true });
            topmenu.addItem({ name: 'nocaps', type: 'button', execute: reditorNoCaps, icon: 'text_smallcaps.png', visible: true });
            topmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            topmenu.addItem({ name: 'padding-top', type: 'slider', execute: reditorPaddingTop, icon: 'text_padding_top.png', visible: true });
            topmenu.addItem({ name: 'padding-left', type: 'slider', execute: reditorPaddingLeft, icon: 'text_padding_left.png', visible: true });
            topmenu.addItem({ name: 'padding-bottom', type: 'slider', execute: reditorPaddingBottom, icon: 'text_padding_bottom.png', visible: true });
            topmenu.addItem({ name: 'padding-right', type: 'slider', execute: reditorPaddingRight, icon: 'text_padding_right.png', visible: true });
            topmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            topmenu.addItem({ name: 'preview', type: 'button', execute: reditorPreview, icon: 'world.png', visible: true });
            topmenu.addItem({ name: 'save', type: 'button', execute: reditorSave, icon: 'disk.png', visible: true });
            topmenu.addItem({ name: null, type: 'linebreak', execute: null, icon: null, visible: true });
            topmenu.addItem({ name: 'imge', type: 'button', execute: reditorImage, icon: 'image.png', visible: true });
            topmenu.addItem({ name: 'table', type: 'button', execute: reditorTable, icon: 'table.png', visible: true });
            topmenu.addItem({ name: 'list', type: 'button', execute: reditorUnorderedList, icon: 'text_list_bullets.png', visible: true });
            topmenu.addItem({ name: 'ordered-list', type: 'button', execute: reditorOrderedList, icon: 'text_list_numbers.png', visible: true });
            topmenu.addItem({ name: 'horizontalrule', type: 'button', execute: reditorHorizontalRule, icon: 'text_horizontalrule.png', visible: true });
            topmenu.addItem({ name: 'link', type: 'button', execute: reditorLink, icon: 'link_add.png', visible: true });
            topmenu.addItem({ name: 'unlink', type: 'button', execute: reditorUnlink, icon: 'link_delete.png', visible: true });
            topmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            topmenu.addItem({ name: 'editcss', type: 'button', execute: reditorEditCss, icon: 'css.png', visible: true });
            topmenu.addItem({ name: 'textcolour', type: 'button', execute: reditorTextColour, icon: 'paintbrush.png', visible: true });
            topmenu.addItem({ name: 'backgroundcolour', type: 'button', execute: reditorBackgroundColour, icon: 'paintcan.png', visible: true });

            if (options.topmenu)
                obj.before(topmenu.html());


            // INITIALIZE REINFO
            var reinfo = document.createElement('div');
            $(reinfo).addClass('ui-corner-bottom');
            $(reinfo).addClass('ui-widget-content');
            $(reinfo).addClass('reditor-reinfo');

            if (options.bottominfo)
                obj.after(reinfo);

            jqueryfyButtons();

            // INITIALIZE POPUP MENU
            var remenu = document.createElement('div');
            $(remenu).attr('class', 'ui-corner-all reditor-remenu ui-widget-header');
            $(remenu).css('opacity', 0);
            var popmenu = new menu(remenu);
            popmenu.addItem({ name: 'bold', type: 'button', execute: reditorBold, icon: 'text_bold.png', visible: true });
            popmenu.addItem({ name: 'italic', type: 'button', execute: reditorItalic, icon: 'text_italic.png', visible: true });
            popmenu.addItem({ name: 'underline', type: 'button', execute: reditorUnderline, icon: 'text_underline.png', visible: true });
            popmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            popmenu.addItem({ name: 'align-left', type: 'button', execute: reditorAlignLeft, icon: 'text_align_left.png', visible: true });
            popmenu.addItem({ name: 'align-center', type: 'button', execute: reditorAlignCenter, icon: 'text_align_center.png', visible: true });
            popmenu.addItem({ name: 'align-right', type: 'button', execute: reditorAlignRight, icon: 'text_align_right.png', visible: true });
            popmenu.addItem({ name: 'align-justify', type: 'button', execute: reditorAlignJustify, icon: 'text_align_justify.png', visible: true });
            popmenu.addItem({ name: null, type: 'separator', execute: null, icon: null, visible: true });
            popmenu.addItem({ name: 'link', type: 'button', execute: reditorLink, icon: 'link_add.png', visible: true });
            popmenu.addItem({ name: null, type: 'linebreak', execute: null, icon: null, visible: true });
            popmenu.addItem({ name: 'strikethrough', type: 'button', execute: reditorStrikethrough, icon: 'text_strikethrough.png', visible: true });
            popmenu.addItem({ name: 'subscript', type: 'button', execute: reditorSubscript, icon: 'text_subscript.png', visible: true });
            popmenu.addItem({ name: 'superscript', type: 'button', execute: reditorSuperscript, icon: 'text_superscript.png', visible: true });
            popmenu.addItem({ name: 'quote', type: 'button', execute: reditorQuote, icon: 'comment.png', visible: true });
            obj.parent().append(popmenu.html());

            // INITIALIZE TABBED VIEW
            if (options.htmltab) {
                var tabcontainer = document.createElement('div');
                var tab1 = document.createElement('span');
                var tab2 = document.createElement('span');

                $(tabcontainer).addClass('reditor-tab-container');

                $(tab1).html('wysiwyg view');
                $(tab2).html('html view');

                $(tab1).addClass('ui-corner-top');
                $(tab1).addClass('ui-state-default');
                $(tab1).attr('style', 'background:#ffffff ! important; color: #000000 ! important');

                $(tab2).addClass('ui-corner-top');
                $(tab2).addClass('ui-state-default');
                $(tab2).addClass('ui-priority-secondary');

                $(tabcontainer).attr('id', 'reditor-tabs');
                $(tabcontainer).append(tab1);
                $(tabcontainer).append(tab2);

                obj.before(tabcontainer);

                $(tab1).click(function() {
                    if (currentView == 'html') {
                        obj.parent().children('.reditor-marker').remove();
                        obj.html(obj.text());
                        obj.parent().children('.reditor-retop').slideDown();
                        obj.parent().children('.reditor-reinfo').slideDown();
                        $(tab2).removeAttr('style');
                        $(tab2).addClass('ui-priority-secondary');
                        $(this).attr('style', 'background:#ffffff ! important; color: #000000 ! important');
                        $(this).removeClass('ui-priority-secondary');

                        currentView = 'wysiwyg';
                        obj.css('font-family', '');
                        // ! debug !
                        if (options.debug)
                            Log('View changed to wysiwyg.');
                        // ! end debug !
                    }
                });

                $(tab2).click(function() {
                    if (currentView == 'wysiwyg') {
                        $('.reditor-marker').remove();
                        var text = obj.html();
                        var text2 = text.replace(/<\bstart\b(.*?)\b>\b/g, function(w) {
                            return w.toLowerCase();
                        });
                        obj.text(text2);
                        obj.parent().children('.reditor-retop').slideUp();
                        obj.parent().children('.reditor-reinfo').slideUp();
                        $(tab1).removeAttr('style');
                        $(this).removeClass('ui-priority-secondary');
                        $(this).attr('style', 'background:#ffffff ! important; color: #000000 ! important');
                        $(tab1).addClass('ui-priority-secondary');

                        currentView = 'html';
                        obj.css('font-family', 'courier new');

                        // ! debug !
                        if (options.debug)
                            Log('View changed to html.');
                        // ! end debug !
                    }
                });

            }

            // CURRENT ELEMENT
            $('.reditor *').live('click', function() {
                // heres still something wrong -> see table. (classes dont vanish) 
                current.instance.find('.reditor-current-element').removeClass('reditor-current-element');
                $(this).addClass('reditor-current-element');
                if (options.debug)
                    Log('calling togglemarker from live click', '#ff0000');
                toggleMarker($(this), current.instance);

                current.element = $(this);
                current.html = $(this).html();
                current.elementstack = new Array();

                // INFOBAR
                ri = current.instance.parent().children('.reditor-reinfo');
                ri.html('');

                var tr = true;
                var el = $(this);
                var f = true;
                var c = 0;

                while (tr) {
                    if (f) {
                        var span = document.createElement('span');
                        $(span).addClass('ui-state-active');
                        $(span).addClass('reinfo-button');
                        $(span).attr('title', c);
                        $(span).html(el[0].tagName);
                        $(span).click(function() {
                            toggleReinfoButton(current, current.instance, $(this));
                        });
                        ri.append(span);

                        current.elementstack[c] = el;
                        c++;
                        f = false;
                    }
                    else {
                        var span = document.createElement('span');
                        $(span).addClass('ui-state-default');
                        $(span).addClass('reinfo-button');
                        $(span).attr('title', c);
                        $(span).html(el[0].tagName);
                        $(span).click(function() {
                            toggleReinfoButton(current, current.instance, $(this));
                        });
                        ri.append(span);

                        current.elementstack[c] = el;
                        c++;
                    }

                    if (el.parent().hasClass('reditor'))
                        tr = false;

                    el = el.parent();
                }
            });

            // OVERWRITE ENTER COMMAND
            obj.keypress(function(e) {
                if (e.which == 13 && currentView == 'html') {
                    // ! debug !
                    if (options.debug)
                        Log('Event: Default keypress event for key ' + e.which +
                            ' has been overwritten and executed.', '#ee9900');
                    // ! end debug !
                    return false;
                }
            });

            // REMOVE MARKER ON SCROLL
            obj.scroll(function() {
                $('.reditor-marker').remove();
                // ! debug !
                if (options.debug)
                    Log('Event: Reditor scrolled.', '#999999');
                // ! end debug !
            });

            obj.mouseup(function(e) {
                var pop = obj.siblings('.reditor-remenu');
                pop.css('opacity', 0);
                pop.css('display', 'none');
                if (rangeSelected() && currentView != 'html') {
                    pop.css('display', 'inline');
                    pop.css('left', e.pageX - pop.outerWidth() / 2);
                    pop.css('top', e.pageY - pop.outerHeight() - 40);

                    jqueryfyButtons();
                    pop.animate({
                        opacity: 1,
                        top: pop.position().top + 20
                    },
                        300
                    );

                    pop.mouseleave(function() {
                        pop.css('display', 'none');
                    });
                    // ! debug !
                    if (options.debug)
                        Log('Show pop-up menu.');
                    // ! end debug !
                }
                // ! debug !
                if (options.debug)
                    Log('Event: Reditor mouseup.', '#999999');
                // ! end debug !
            });
        });

        /* ==========================================================================================
        PRIVATE FUNCTIONS
        ==========================================================================================*/
        function rangeParent(range) {
            if ($.browser.msie)
                return range.parentElement();
            else
                return range.commonAncestorContainer;
        }

        function focusElement(element) {
            if (window.getSelection) {
                var selection = window.getSelection();
                var range = document.createRange();
                range.setStart(element, 1);
                range.setEnd(element, 1);
                selection.removeAllRanges();
                selection.addRange(range);
            }
            else if (document.selection && document.body.createTextRange) {
                /*var range = document.body.createTextRange();
                range.moveToBookmark(storedSelection);
                range.select();*/
            }
        }

        function rangeSelected() {
            try {
                if ($.browser.msie) {
                    var selection = document.selection;
                    var range = selection.createRange();
                    if (range.text.length > 0)
                        return true;
                    else
                        return false;
                }
                else {
                    var selection = window.getSelection();
                    if (selection.toString().length > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (err) {
                return false;
            }
        }

        function saveSelection() {
            if (window.getSelection) {
                var selection = window.getSelection();
                if (selection.rangeCount > 0) {
                    var selectedRange = selection.getRangeAt(0);
                    return selectedRange.cloneRange();
                }
                else {
                    return null;
                }
            }
            else if (document.selection) {
                var selection = document.selection;
                if (selection.type.toLowerCase() == 'text') {
                    return selection.createRange().getBookmark();
                }
                else {
                    return null;
                }
            }
            else {
                return null;
            }
        }

        function loadSelection(storedSelection) {
            if (storedSelection) {
                if (window.getSelection) {
                    var selection = window.getSelection();
                    selection.removeAllRanges();
                    selection.addRange(storedSelection);
                }
                else if (document.selection && document.body.createTextRange) {
                    var range = document.body.createTextRange();
                    range.moveToBookmark(storedSelection);
                    range.select();
                }
            }
        }

        function inject(target, element) {
            if (target[0].tagName.toLowerCase() == 'td')
                target.append(element);
            else
                target.after(element);
        }

        function jqueryfyButtons() {
            if (!$.browser.msie) {
                $('.reditor-remenu-icon, .reditor-remenu-icon-split, .reditor-remenu-select').addClass('ui-priority-secondary');

                $('.reditor-remenu-icon, .reditor-remenu-icon-split, .reditor-remenu-select').hover(function() {
                    $(this).removeClass('ui-priority-secondary');
                    $(this).addClass('ui-state-default');
                    $(this).addClass('ui-corner-all');
                },
                function() {
                    $(this).addClass('ui-priority-secondary');
                    $(this).removeClass('ui-state-default');
                    $(this).removeClass('ui-corner-all');
                });
            }
            else {
                $('.reditor-remenu-icon, .reditor-remenu-icon-split, .reditor-remenu-select').css('margin', '2px');
            }
        }

        function toggleReinfoButton(current, obj, button) {
            $(button).parent().children('.reinfo-button').removeClass('ui-state-active').addClass('ui-state-default');
            $(button).removeClass('ui-state-default').addClass('ui-state-active');
            current.element = current.elementstack[$(button).attr('title')];
            $(button).parent().children('.reinfo-button').removeClass('reditor-current-element');
            current.element.addClass('reditor-current-element');
            // ! debug !
            if (options.debug)
                Log('Function: toggleReinfoButton. ' + current.element + ' is now active.', '#009900');
            // ! end debug !
            toggleMarker(current.element, obj);
        }

        /* ==========================================================================================
        HELPER-FUNCTIONS FOR INPUTS AND INTERFACE
        ==========================================================================================*/

        function toggleMarker(Element, obj) {
            obj.parent().children('.reditor-marker').remove();
            var pointer = document.createElement('div');
            var bottom = document.createElement('div');

            $(pointer).attr('contentEditable', false);
            var offset = Element.offset();
            offset.top = offset.top - 15;
            $(pointer).css(offset);
            $(pointer).width(Element.outerWidth() - 2);
            $(pointer).addClass('reditor-marker');
            $(pointer).addClass('ui-corner-top');
            $(pointer).addClass('ui-state-highlight');
            $(pointer).append('<span>' + Element[0].tagName + '</span>');
            $(pointer).append('<img id="reditor-delete-bullet" src="' + options.basepath + 'css/img/ico/bullet_delete.png" alt="Delete" />');
            $(pointer).append('<img id="reditor-dublicate-bullet" src="' + options.basepath + 'css/img/ico/bullet_add.png" alt="Dublicate" />'); ;
            obj.parent().append(pointer);

            offset.top = offset.top + 15 + Element.outerHeight();
            $(bottom).css(offset);
            $(bottom).width(Element.outerWidth() - 2);
            $(bottom).height(5);
            $(bottom).addClass('reditor-marker');
            $(bottom).addClass('ui-corner-bottom');
            $(bottom).addClass('ui-state-highlight');
            obj.parent().append(bottom);

            $('#reditor-delete-bullet').click(function() {
                Element.remove();
                $('.reditor-marker').remove();
                // ! debug !
                if (options.debug)
                    Log('Element ' + Element[0].tagName + ' has been removed by user.');
                // ! end debug !
            });
            $('#reditor-dublicate-bullet').click(function() {
                Element.after('<' + Element[0].tagName + '>' + Element.html() + '</' + Element[0].tagName + '>');
                // ! debug !
                if (options.debug)
                    Log('Element ' + Element[0].tagName + ' has been dublicated by user.');
                // ! end debug !
            });
            // ! debug !
            if (options.debug)
                Log('Function: toggleMarker. ' + Element[0].tagName + '\'s marker is now shown.', '#009900');
            // ! end debug !
        }

        function createTextBox(id, name, description) {
            var d = document.createElement('div');

            var i = document.createElement('input');
            i.setAttribute('type', 'text');
            i.setAttribute('id', id);
            i.setAttribute('className', 'reditor-dialog-input');
            i.setAttribute('class', 'reditor-dialog-input');

            var l = document.createElement('label');
            l.appendChild(document.createTextNode(name));

            var p = document.createElement('p');
            p.setAttribute('className', 'reditor-dialog-description');
            p.setAttribute('class', 'reditor-dialog-description');
            p.appendChild(document.createTextNode(description));

            d.appendChild(l);
            d.appendChild(i);
            d.appendChild(p);
            return d;
        }

        function createCheckBox(id, name, description) {
            var d = document.createElement('div');

            var i = document.createElement('input');
            i.setAttribute('type', 'checkbox');
            i.setAttribute('id', id);
            i.setAttribute('className', 'reditor-dialog-input-noborder');
            i.setAttribute('class', 'reditor-dialog-input-noborder');

            var l = document.createElement('label');
            l.appendChild(document.createTextNode(name));

            var p = document.createElement('p');
            p.setAttribute('className', 'reditor-dialog-description');
            p.setAttribute('class', 'reditor-dialog-description');
            p.appendChild(document.createTextNode(description));

            d.appendChild(l);
            d.appendChild(i);
            d.appendChild(p);
            return d;
        }

        function createTextArea(id, name, description) {
            var d = document.createElement('div');

            var i = document.createElement('textarea');
            i.setAttribute('id', id);
            i.setAttribute('className', 'reditor-dialog-input');
            i.setAttribute('class', 'reditor-dialog-input');

            var l = document.createElement('label');
            l.appendChild(document.createTextNode(name));

            var p = document.createElement('p');
            p.setAttribute('className', 'reditor-dialog-description');
            p.setAttribute('class', 'reditor-dialog-description');
            p.appendChild(document.createTextNode(description));

            d.appendChild(l);
            d.appendChild(i);
            d.appendChild(p);
            return d;
        }

        function createSelect() {
        }

        function createColumn() {
            var c = document.createElement('div');
            $(c).attr('style', 'float:left; margin-right:10px;');
            return c;
        }

        // INTEGRATED FILE BROWSER
        function openFolder(Folder, Connector, Target) {
            var files = new Array();
            var folders = new Array();
            files.length = 0;
            folders.length = 0;
            $.ajaxSetup({ cache: false });
            $.getJSON(Connector + "?path=" + Folder,
            { folder: Folder },
            function(data) {
                $.each(data.files, function(i, item) {
                    files[i] = item;
                });
                $.each(data.folders, function(i, item) {
                    folders[i] = item;
                });
                $.ajaxSetup({ cache: true });
                createList(Connector, Target, folders, files);
            }
        );

            return true;
        };

        function createList(Connector, Target, folders, files) {
            var div = document.createElement('div');
            div.setAttribute('className', 'reditor-browser-explorer');
            div.setAttribute('class', 'reditor-browser-explorer');

            var img = document.createElement('img');
            img.setAttribute('className', 'reditor-browser-preview');
            img.setAttribute('class', 'reditor-browser-preview');

            for (var i = 0, len = folders.length; i < len; ++i) {
                try {
                    var folder = document.createElement('a');
                    folder.appendChild(document.createTextNode(folders[i].name));
                    folder.setAttribute('className', 'reditor-browser-folder');
                    folder.setAttribute('class', 'reditor-browser-folder');
                    folder.setAttribute('href', folders[i].path);
                    div.appendChild(folder);
                }
                catch (err) {
                }
            }
            for (var i = 0, len = files.length; i < len; ++i) {
                try {
                    var file = document.createElement('a');
                    if (files[i].name.length > 16)
                        files[i].name = files[i].name.substr(0, 13) + ' ...';


                    file.appendChild(document.createTextNode(files[i].name));
                    file.setAttribute('className', 'reditor-browser-file reditor-browser-' + files[i].filetype);
                    file.setAttribute('class', 'reditor-browser-file reditor-browser-' + files[i].filetype);
                    file.setAttribute('href', files[i].path);
                    file.setAttribute('title', files[i].size + ' MByte');
                    div.appendChild(file);
                }
                catch (err) {
                }
            }

            $(Target).children().remove();
            $(Target)[0].appendChild(img);
            $(Target)[0].appendChild(div);
            $(Target).append('<div style="clear:both"></div>');

            $('.reditor-browser-folder').click(function() {
                openFolder($(this).attr('href'), Connector, Target);
                return false;
            });

            $('.reditor-browser-file').click(function() {
                $('#remenu-action-image-path').val($(this).attr('href'));
                $('.reditor-browser-preview').attr('src', $(this).attr('href'));
                return false;
            });
        }

    };
})(jQuery);