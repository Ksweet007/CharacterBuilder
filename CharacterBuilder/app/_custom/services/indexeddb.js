define('services/indexeddb', function(require) {
	var _i = {
		ko: require('knockout'),
		$: require('jquery')
	};

	function IndexDbCls() {
		var self = this;
		//https://code.tutsplus.com/tutorials/working-with-indexeddb--net-34673
	}

	IndexDbCls.prototype.isCompatable = function() {
		if ("indexedDB" in window) {
			return true;
		}
		return false;
	};

	IndexDbCls.prototype.createIndex = function(objectStore) {
		objectStore.createIndex("name", "name", {unique: true});
		objectStore.createIndex("powersource", "powersource", {	unique: false})
	};

	IndexDbCls.prototype.createTable = function(database) {
		var tablename = ["classes"];
		if (!database.objectStoreNames.contains(tablename)) {
			var objectStore = database.createObjectStore(tablename, {autoIncrement: true});
			//first arg is Index name, second is Column
			objectStore.createIndex("name", "name", {unique: true});
			objectStore.createIndex("powersource", "powersource", {	unique: false})
		}
	};

	IndexDbCls.prototype.createDatabase = function(e) {
		var thisDB = e.target.result;
		return thisDB;
	};

	IndexDbCls.prototype.deleteDatabase = function(database) {
		var req = indexedDB.deleteDatabase(database);
		req.onsuccess = function() {
			console.log("Deleted " + database + " database successfully");
		};
		req.onerror = function() {
			console.log("Couldn't delete " + database + " database");
		};
		req.onblocked = function() {
			console.log("Couldn't delete " + database + " database due to the operation being blocked");
		}
	};

	IndexDbCls.prototype.addObject = function(database, table, objtosave) {
		//stringify then parse
		var stringifiedObj = JSON.stringify(objtosave);
		if (stringifiedObj !== "") {
			var parsedJSON = JSON.parse(stringifiedObj);
			objtosave = parsedJSON;
		}
		var transaction = database.transaction([table], "readwrite");
		var store = transaction.objectStore(table);

		//Perform the add
		var request = store.add(objtosave);
		request.onerror = function(e) {
			console.log("Couldn't add object - ", e.target.error.name);
		}

		request.onsuccess = function(e) {
			console.log("Object added successfully")
		}
	};

	IndexDbCls.prototype.getObj = function(database, table) {
		var transaction = database.transaction([table], "readonly");
		var objectStore = transaction.objectStore(table);

		//x is key by default
		var key = 1;
		var ob = objectStore.get(Number(key));

		//returns our requested object
		ob.onsuccess = function(e) {
			console.log("Found Object - ", e.target.result)
		}
	};

	IndexDbCls.prototype.getList = function(database, table) {
		var transaction = database.transaction([table], "readonly");
		var objectStore = transaction.objectStore(table);

		var cursor = objectStore.openCursor();

		cursor.onsuccess = function(e) {
			var result = e.target.result;
			if (result) {
				console.log("Key", result.key);
				console.log("Data", result.value);
				result.continue();
			}
		}

	};

	IndexDbCls.prototype.search = function(){
		var transaction = self.db.transaction(["classes"], "readonly");
		var store = transaction.objectStore("classes");
		var index = store.index("name");

		//name is some value
		var name= "Bard";
		var request = index.get(name);
		request.onsuccess = function(e) {

			var result = e.target.result; //THe object found with our request/query
		}
	};

	return new IndexDbCls();
});
