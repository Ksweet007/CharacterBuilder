define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        charajax: require('_custom/services/WebAPI'),
        deferred: require('_custom/deferred'),
        alert: require('_custom/services/alert'),
        confirmdelete: require('confirmdelete/confirmdelete'),
        newcampaignprompt: require('newcampaignprompt/newcampaignprompt'),
    };

    return function () {
        var self = this;
        self.campaignId = _i.ko.observable(0);
        self.PlayerCards = _i.ko.observableArray([]);
        self.CardsToShow = _i.ko.computed(function () {
            var returnList = self.PlayerCards();
            return returnList;
        });

        self.selectedCard = _i.ko.observable();
        self.isEditing = _i.ko.observable(false);
        self.isAddingNew = _i.ko.observable(false);

        self.activate = function (campaignId) {
            self.campaignId(campaignId);
            return self.getPageData().done(function () {

            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getCards());

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
        };

        self.getCards = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/dungeonmaster/PlayerInfoCards/' + self.campaignId()).done(function (response) {
                var mapped = _i.ko.mapping.fromJS(response);
                self.PlayerCards(mapped());
                self.PlayerCards.sort(function (left, right) { return left.PlayerName === right.PlayerName ? 0 : (left.PlayerName < right.PlayerName ? -1 : 1) });

                deferred.resolve();
            });
            return deferred;
        };

        self.selectCard = function (cardSelected) {
            self.selectedCard(cardSelected);
            _i.alert.showAlert({ type: "success", message: "Editing " + cardSelected.CharacterName() });
            self.isEditing(true);
        };


        self.addNew = function () {
            self.isEditing(true);
            self.isAddingNew(true);
            var data = {
                PlayerName: _i.ko.observable(''),
                CharacterName: _i.ko.observable(''),
                ArmorClass: _i.ko.observable(0),
                HitPoints: _i.ko.observable(0),
                PassivePerception: _i.ko.observable(0)
            };

            self.selectedCard(data);
        };

        self.cancel = function() {
            self.isEditing(false);
        };

        self.save = function (card) {            
            var datatosave = {
                CampaignId: self.campaignId(),
                ArmorClass: card.ArmorClass(),
                CharacterName: card.CharacterName(),
                HitPoints: card.HitPoints(),
                PassivePerception: card.PassivePerception(),
                PlayerName: card.PlayerName()
            };

            _i.charajax.post('api/dungeonmaster/CreatePlayerCard/', datatosave).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Created new Character Card!" });
                self.PlayerCards.push(response);
                self.PlayerCards.sort(function (left, right) { return left.PlayerName === right.PlayerName ? 0 : (left.PlayerName < right.PlayerName ? -1 : 1) });
                self.isEditing(false);                
            });
        };

        self.deleteCard = function (obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/dungeonmaster/DeleteCard/' + obj.Id(), '').done(function (response) {
                        self.PlayerCards.remove(obj);
                        _i.alert.showAlert({ type: "error", message: "Campaign Deleted" });
                    });
                }
            });
        };
    }
});
