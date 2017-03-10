define(function (require) {
    var _i = {
        ko: require('knockout'),
        $: require('jquery'),
        moment: require('moment'),
        charajax: require('_custom/services/WebAPI'),
        list: require('_custom/services/listmanager'),
        deferred: require('_custom/deferred'),
        alert: require('_custom/services/alert'),
        globals: require('_custom/services/builderglobals'),
        confirmdelete: require('confirmdelete/confirmdelete')
    };

    return function () {
        var self = this;
        
        /*==================== BASE DATA ====================*/
        self.characterSheets = _i.ko.observableArray([]);

        /*==================== PAGE STATE/FILTERED ITEMS ====================*/
        self.selectedSheet = _i.ko.observable();
     
        self.viewingDetails = _i.ko.observable(false);
        self.showAll = _i.ko.observable(true);
        
        self.characterSheetsToShow = _i.ko.computed(function () {
            var returnList = self.characterSheets();
            return returnList;
        });

        self.activate = function () {
            return self.getPageData().done(function () {

            });
        };

        self.getPageData = function () {
            var deferred = _i.deferred.create();
            var promise = _i.deferred.waitForAll(self.getCharacterSheets());

            promise.done(function () {
                deferred.resolve();
            });

            return deferred;
        };

        self.getCharacterSheets = function () {
            var deferred = _i.deferred.create();
            _i.charajax.get('api/charactersheet/GetUserSheets').done(function (response) {
                response.forEach(function (sheet) {
                    sheet.createdDateFormatted = moment(sheet.CreatedDate).format('LLL');
                    self.addAbilityScoreIncreasesToScores(sheet);
                    self.calculateAbilityModifiers(sheet);
                });
                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets(mapped());
                deferred.resolve();
            });
            return deferred;
        };

        self.addAbilityScoreIncreasesToScores = function (sheet) {
            sheet.strBonus = 0;
            sheet.dexBonus = 0;
            sheet.conBonus = 0;
            sheet.intBonus = 0;
            sheet.wisBonus = 0;
            sheet.chaBonus = 0;
            sheet.AbilityScoreIncreases.forEach(function (item) {
                switch (item.Name) {
                    case "Strength":
                        sheet.Strength += item.IncreaseAmount;
                        sheet.strBonus = item.IncreaseAmount;
                        break;
                    case "Dexterity":
                        sheet.Dexterity += item.IncreaseAmount;
                        sheet.dexBonus = item.IncreaseAmount;
                        break;
                    case "Constitution":
                        sheet.Constitution += item.IncreaseAmount;
                        sheet.conBonus = item.IncreaseAmount;
                        break;
                    case "Intelligence":
                        sheet.Intelligence += item.IncreaseAmount;
                        sheet.intBonus = item.IncreaseAmount;
                        break;
                    case "Wisdom":
                        sheet.Wisdom += item.IncreaseAmount;
                        sheet.wisBonus = item.IncreaseAmount;
                        break;
                    case "Charisma":
                        sheet.Charisma += item.IncreaseAmount;
                        sheet.chaBonus = item.IncreaseAmount;
                        break;
                }
            });
        };

        self.calculateAbilityModifiers = function(sheet) {
            sheet.StrengthMod = Math.floor((sheet.Strength - 10) / 2);
            sheet.DexterityMod = Math.floor((sheet.Dexterity - 10) / 2);
            sheet.ConstitutionMod = Math.floor((sheet.Constitution - 10) / 2);
            sheet.IntelligenceMod = Math.floor((sheet.Intelligence - 10) / 2);
            sheet.WisdomMod = Math.floor((sheet.Wisdom - 10) / 2);
            sheet.CharismaMod = Math.floor((sheet.Charisma - 10) / 2);
        };

        self.calculateSingleModifier = function(abil) {
            return Math.floor((abil - 10) / 2);
        };

        self.setScoreOnRoll = function(abil,bonus,mod) {
            var rolledValue = self.rollAbilityScore();
            var newScore = rolledValue + bonus;
            var newMod = Math.floor((newScore - 10) / 2);

            abil(newScore);
            mod(newMod);
        };

        self.rollStr = function () {
            self.setScoreOnRoll(self.selectedSheet().Strength, self.selectedSheet().strBonus(), self.selectedSheet().StrengthMod);           
        };

        self.rollDex = function () {
            self.setScoreOnRoll(self.selectedSheet().Dexterity,self.selectedSheet().dexBonus(), self.selectedSheet().DexterityMod);
        };

        self.rollCon = function () {
            self.setScoreOnRoll(self.selectedSheet().Constitution,self.selectedSheet().conBonus(), self.selectedSheet().ConstitutionMod);
        };

        self.rollInt = function () {
            self.setScoreOnRoll(self.selectedSheet().Intelligence,self.selectedSheet().intBonus(), self.selectedSheet().IntelligenceMod);
        };

        self.rollWis = function () {
            self.setScoreOnRoll(self.selectedSheet().Wisdom, self.selectedSheet().wisBonus(),self.selectedSheet().WisdomMod);
        };

        self.rollCha = function () {
            self.setScoreOnRoll(self.selectedSheet().Charisma, self.selectedSheet().chaBonus(),self.selectedSheet().CharismaMod);
        };

        self.rollAbilityScore = function () {
            var rolls = [];

            for (var i = 0; i < 4; i++) {
                var d6 = 1 + Math.floor(Math.random() * 6);
                rolls.push(d6);
            }

            rolls.sort();        
            rolls.shift();

            var scoreTotal = 0;
            for (var s in rolls) {
                scoreTotal += rolls[s];
            }
            return scoreTotal;
        };

        self.viewMoreDetails = function (bgSelected) {
            self.selectedSheet(bgSelected);
            self.viewingDetails(true);
        };

        self.closeMoreDetails = function () {
            self.viewingDetails(false);
        };

        self.deleteSheet = function(obj) {
            _i.confirmdelete.show().then(function (response) {
                if (response.accepted) {
                    _i.charajax.delete('api/charactersheet/DeleteSheet/' + obj.Id(), '').done(function (response) {
                        var alertMsg = "Character Sheet for " + obj.CharacterName() + " Deleted";
                        self.characterSheets.remove(obj);
                        document.cookie = "SheetBeingWorked=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
                        _i.alert.showAlert({ type: "error", message: alertMsg });
                    });
                }
            });
        };
        
        self.selectSheetToEdit = function (sheetToEdit) {
            self.selectedSheet(sheetToEdit);

            _i.globals.setSheetToEdit(self.selectedSheet().Id());
            _i.globals.createCookie("SheetBeingWorked",self.selectedSheet().Id());
            
            var editMessage = "Currently Editing";
            if (sheetToEdit.CharacterName() != null) {
                editMessage += " " + sheetToEdit.CharacterName();
            }
            
            _i.alert.showAlert({ type: "success", message: editMessage });
        };

        self.showAlertAndOpenEditor = function () {
            var alertConfig = { type: "success", message: "New Character Added!" };
            _i.alert.showAlert(alertConfig);
            self.viewingDetails(true);
        };

        self.addNew = function() {
            _i.charajax.post('api/charactersheet/CreateNewSheet', '').done(function(response) {
                window.builder.global_sheetid = response.Id;
                response.createdDateFormatted = moment(response.CreatedDate).format('LLL');
                
                var mapped = _i.ko.mapping.fromJS(response);

                self.characterSheets.push(mapped);
                self.selectedSheet(mapped);
                self.showAlertAndOpenEditor();
                
            });
        };

    }
});
