const newman = require('newman'); 
const parallel = require('run-parallel')
const fs = require('fs');

var number = 0;
// call newman.run to pass `options` object and wait for callback
var myfunc = () => {
    console.log(number++);

    newman.run({
        collection: require('./UfabMicroservice.postman_collection.json'),
        environment :require('./development.postman_environment'),
        reporters: 'cli'
    }, function (err) {
        if (err) { 
            console.log("Error" + err);
        }
    });
}

var tasks = [];
for(var i=0; i<100;i++)
{
    tasks.push(myfunc);
}
parallel(tasks);

