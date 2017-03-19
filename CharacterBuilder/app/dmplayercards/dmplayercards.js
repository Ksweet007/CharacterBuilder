define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        charajax: require('_custom/services/WebAPI'),
        deferred: require('_custom/deferred'),
        alert: require('_custom/services/alert'),
        confirmdelete: require('confirmdelete/confirmdelete'),
        newcampaignprompt: require('newcampaignprompt/newcampaignprompt'),
        mapper: require('_custom/services/mapper')
    };

    return function () {
        var self = this;
        self.campaignId = _i.ko.observable(0);
        self.PlayerCards = _i.ko.observableArray([]);
        //self.CardsToShow = _i.ko.computed(function () {
        //    var returnList = self.PlayerCards();
        //    return returnList;
        //});
        self.selectedCard = _i.ko.observable();
        self.previousState = _i.ko.observable();
        self.isEditing = _i.ko.observable(false);
        self.isAddingNew = _i.ko.observable(false);

        self.activate = function (campaignId) {
            self.campaignId(campaignId);
            return self.getPageData().done(function () {

                self.PlayerCards.subscribe(function (changes) {
                    changes.forEach(function (change) {
                        if (change.status === 'added') {
                            self.PlayerCards().sort(function (left, right) { return left.PlayerName === right.PlayerName ? 0 : (left.PlayerName < right.PlayerName ? -1 : 1) });
                        } else if (change.status === 'deleted') {
                            self.PlayerCards().sort(function (left, right) { return left.PlayerName === right.PlayerName ? 0 : (left.PlayerName < right.PlayerName ? -1 : 1) });
                        }
                    });

                }, null, "arrayChange");

                var mappedCards = _i.ko.utils.arrayMap(self.data, function (card) {
                    var mappedCard = _i.ko.mapping.fromJS(card);
                    return new _i.mapper.MapPlayerCard(mappedCard, card);
                });
                self.PlayerCards(mappedCards);

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
                self.data = response;
                var mapped = _i.ko.mapping.fromJS(response);
                self.PlayerCards(mapped());

                deferred.resolve();
            });
            return deferred;
        };

        self.selectCard = function (cardSelected) {
            cardSelected.IsEditing(true);
            self.selectedCard(cardSelected);
            _i.alert.showAlert({ type: "success", message: "Editing " + cardSelected.CharacterName() });
            self.isEditing(true);
        };

        self.addNew = function () {
            self.isAddingNew(true);
            var data = {
                PlayerName: _i.ko.observable(''),
                CharacterName: _i.ko.observable(''),
                ArmorClass: _i.ko.observable(0),
                HitPoints: _i.ko.observable(0),
                PassivePerception: _i.ko.observable(0),
                IsEditing: _i.ko.observable(false)
                
            };

            self.selectedCard(data);
        };

        self.cancel = function (obj) {
            obj.IsEditing(false);
            self.selectedCard(null);
            self.isEditing(false);
            self.isAddingNew(false);
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

                var mappedResponse = _i.ko.mapping.fromJS(response);
                var mappedCard = new _i.mapper.MapPlayerCard(mappedResponse, card);
                self.PlayerCards.push(mappedCard);
                self.isAddingNew(false);
            });
        };

        self.saveEdit = function (card) {
            var datatosave = {
                Id: card.Id(),
                CampaignId: self.campaignId(),
                ArmorClass: card.ArmorClass(),
                CharacterName: card.CharacterName(),
                HitPoints: card.HitPoints(),
                PassivePerception: card.PassivePerception(),
                PlayerName: card.PlayerName()
            };

            _i.charajax.put('api/dungeonmaster/EditPlayerCard/', datatosave).done(function (response) {
                _i.alert.showAlert({ type: "success", message: "Edit Saved" });
                self.isEditing(false);
                card.PrevCharacterName(response.CharacterName);
                card.IsEditing(false);
            });
        };

        self.deleteCard = function (obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/dungeonmaster/DeleteCard/' + obj.Id(), '').done(function (response) {
                        self.PlayerCards.remove(obj);
                        _i.alert.showAlert({ type: "error", message: "Player Card Deleted" });
                    });
                }
            });
        };
    }
});
