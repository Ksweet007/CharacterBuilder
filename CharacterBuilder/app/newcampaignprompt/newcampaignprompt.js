define(function (require) {
    var _i = {
        ko: require('knockout'),
        dialog: require('plugins/dialog')
    };
    var NewCampPrompt = function() {
        var self = this;
        self.CampaignName = _i.ko.observable('');
    };

    NewCampPrompt.show = function () {
        var self = this;
        return _i.dialog.show(new NewCampPrompt());
    };
    
    NewCampPrompt.prototype.Save = function() {
        var self = this;
        _i.dialog.close(self, self.CampaignName());
    }

    NewCampPrompt.prototype.Cancel = function () {
        var self = this;
        _i.dialog.close();
    }


    return NewCampPrompt;
});
