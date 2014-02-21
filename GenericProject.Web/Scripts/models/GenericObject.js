var GenericObject = Base.extend({
  constructor: function(name) {
    this.name = name;
  },
  
  name: "",
  
  hello: function() {
    this.say("Yum!");
  },
  
  say: function(message) {
    alert(this.name + ": " + message);
  }
});