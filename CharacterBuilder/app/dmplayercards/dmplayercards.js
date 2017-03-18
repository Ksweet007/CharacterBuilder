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
        
        self.activate = function (campaignId) {
            self.campaignId(campaignId);
            return self.getPageData().done(function () {

                self.isViewingDetails = _i.ko.computed(function() {
                    return self.selectedCard() !== undefined && self.selectedCard().Id > 0;
                });
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

                deferred.resolve();
            });
            return deferred;
        };
        
        self.selectCard = function (cardSelected) {
            self.selectCard(cardSelected);
        };


        self.addNew = function () {

            self.PlayerCards.push({ PlayerName: "dfdddd" });
            //_i.newcampaignprompt.show().then(function (response) {
            //    _i.charajax.post('api/dungeonmaster/CreateCampaign/' + response).done(function (response) {
            //        _i.alert.showAlert({ type: "success", message: "Created new Campaign!" });

            //        window.location.href = '#dmplayercard/' + response.Id;
            //    });
            //});
        };

        self.deleteCampaign = function (obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/dungeonmaster/DeleteCampaign/' + obj.Id(), '').done(function (response) {
                        self.campaigns.remove(obj);
                        _i.alert.showAlert({ type: "error", message: "Campaign Deleted" });
                    });
                }
            });
        };
    }
});
