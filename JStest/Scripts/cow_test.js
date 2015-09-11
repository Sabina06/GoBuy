var expect = chai.expect;

describe("Cow", function() {
  describe("constructor", function() {
    it("should have a default name", function() {
      var cow = new Cow();
      expect(cow.name).to.equal("Anon cow");
    });

    it("should set cow's name if provided", function() {
      var cow = new Cow("Kate");
      expect(cow.name).to.equal("Kate");
    });
  });

  describe("#greets", function() {
    it("should throw if no target is passed in", function() {
      expect(function() {
        (new Cow()).greets();
      }).to.throw(Error);
    });

    it("should greet passed target", function() {
      var greetings = (new Cow("Kate")).greets("Baby");
      expect(greetings).to.equal("Kate greets Baby");
    });
  });

  describe("#lateGreets", function() {
    it("should pass an error if no target is passed", function(done) {
      (new Cow()).lateGreets(null, function(err, greetings) {
        expect(err).to.be.an.instanceof(Error);
        done();
      });
    });

    it("should greet passed target after one second", function(done) {
      (new Cow("Kate")).lateGreets("Baby", function(err, greetings) {
        expect(greetings).to.equal("Kate greets Baby");
        done();
      });
    });
  });


});
