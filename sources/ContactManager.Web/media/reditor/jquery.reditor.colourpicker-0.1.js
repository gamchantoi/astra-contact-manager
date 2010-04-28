(function($) {
    $.fn.recolour = function(options) {
        var defaults = {
            ok: function(data) { },
            cancel: function(data) { data.remove(); },
            close: true
        };
        var options = $.extend(defaults, options);

        var colour;

        return this.each(function() {
            var obj = $(this);

            obj.css('padding', '20px');
            obj.css('font-size', '50%');
            obj.css('width', '170px');
            obj.css('border', '2px solid #999999');
            obj.addClass('ui-corner-all');
            obj.addClass('ui-widget-content');

            var red = document.createElement('div');
            var green = document.createElement('div');
            var blue = document.createElement('div');

            $(red).attr('class', 'recolour-slider');
            $(green).attr('class', 'recolour-slider');
            $(blue).attr('class', 'recolour-slider');

            $(red).attr('id', 'recolour-slider-red');
            $(green).attr('id', 'recolour-slider-green');
            $(blue).attr('id', 'recolour-slider-blue');

            obj.append(red);
            obj.append(green);
            obj.append(blue);

            var hexBox = document.createElement('input');
            $(hexBox).attr('type', 'text');
            $(hexBox).attr('id', 'recolour-hex-box');
            $(hexBox).attr('style', 'float:left;clear:left;width:68px;margin:5px;padding:2px; border:1px solid #555555;text-align:center;font-size:1.5em');
            $(hexBox).addClass('ui-corner-all');
            $(hexBox).keyup(function() {
                if ($(this).val().length == 7) {
                    $('#recolour-preview').css('background-color', $(this).val());
                    RGBFromHex($(this).val());
                }
            });
            obj.append(hexBox);

            var preview = document.createElement('div');
            $(preview).attr('id', 'recolour-preview');
            $(preview).attr('class', 'ui-corner-all');
            $(preview).attr('style', 'float:left;margin:5px;width:71px;height:19px;background-image:none;border:1px solid #555555');
            obj.append(preview);

            var cancel = document.createElement('button');
            $(cancel).css('margin', '5px');
            $(cancel).css('margin-bottom', '0px');
            $(cancel).css('margin-top', '10px');
            $(cancel).css('float', 'right');
            $(cancel).addClass('ui-state-default');
            $(cancel).html('Cancel');
            $(cancel).click(function() { options.cancel(obj); });
            obj.append(cancel);

            var ok = document.createElement('button');
            $(ok).css('margin', '5px');
            $(ok).css('margin-bottom', '0px');
            $(ok).css('margin-top', '10px');
            $(ok).css('float', 'right');
            $(ok).addClass('ui-state-default');
            $(ok).html('Ok');
            $(ok).click(function() {
                options.ok(colour);
                if (options.close) {
                    obj.remove()
                }
            });
            obj.append(ok);

            $('#recolour-slider-red, #recolour-slider-green, #recolour-slider-blue').slider({
                orientation: 'horizontal',
                range: "min",
                max: 255,
                value: 127,
                slide: refreshPreview,
                change: refreshPreview
            });

            $('.recolour-slider').css('margin', '5px');
            $('.recolour-slider').css('float', 'left');
            $('.recolour-slider').css('clear', 'left');
            $('.recolour-slider').css('width', '156px');

            $('#recolour-slider-red .ui-slider-range').css('background', '#ff0000');
            $('#recolour-slider-green .ui-slider-range').css('background', '#00ff00');
            $('#recolour-slider-blue .ui-slider-range').css('background', '#0000ff');

            obj.append('<div style="clear:both"></div>');
        });

        function RGBFromHex(hex) {
            hex2 = hex.replace('#', '');
            var r = parseInt(hex2.substring(0, 2), 16);
            var g = parseInt(hex2.substring(2, 4), 16);
            var b = parseInt(hex2.substring(4, 6), 16);

            $('#recolour-slider-red').slider('option', 'value', r);
            $('#recolour-slider-green').slider('option', 'value', g);
            $('#recolour-slider-blue').slider('option', 'value', b);
        }

        function hexFromRGB(r, g, b) {
            var hex = [
			r.toString(16),
			g.toString(16),
			b.toString(16)
		];
            $.each(hex, function(nr, val) {
                if (val.length == 1) {
                    hex[nr] = '0' + val;
                }
            });
            return hex.join('').toUpperCase();
        }
        function refreshPreview() {
            var red = $('#recolour-slider-red').slider('value');
            var green = $('#recolour-slider-green').slider('value');
            var blue = $('#recolour-slider-blue').slider('value');
            var hex = hexFromRGB(red, green, blue);
            $('#recolour-preview').css('background-color', '#' + hex);
            $('#recolour-hex-box').val('#' + hex);
            colour = '#' + hex;
        }

    };
})(jQuery);