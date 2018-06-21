(function ($) {
    function Contact() {
        var $this = this;

        function initilizeModel() {
            console.log('text2');
            $("#modal-action-contact").on('loaded.bs.modal', function (e) {
                console.log('text1');
                }).on('hidden.bs.modal', function (e) {
                    console.log('text');
                    $(this).removeData('bs.modal');
                });            
        }       
        $this.init = function () {
            initilizeModel();
        }
    }
    $(function () {
        var self = new Contact();
        self.init();
        
    })
}(jQuery))
