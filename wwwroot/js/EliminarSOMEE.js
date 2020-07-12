$(document).ready(function () {
    SomeeAdsRemover();
    adsRemover();
    $("body").removeAttr('style'); //Atributo para Que no haga el Padding-Right
});

function SomeeAdsRemover() {
    $("center").each(function () {
        if ($(this).html() == '<a href="http://somee.com">Web hosting by Somee.com</a>') {
            $('script[src="http://ads.mgmt.somee.com/serveimages/ad2/WholeInsert4.js"]').remove();
            $('script#last-script').nextAll('div').remove(); // last tag before </body>   
            $(this).next().remove();
            $(this).next().next().remove();
            $(this).next().next().next().remove();
            $(this).remove();

            return false;
        }
        $("div[style='opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;']").remove();
        $("div[style='position: fixed; z-index: 2147483647; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: transparent; margin: 0px; padding: 0px;']").remove();
        $("div[style='height: 65px;']").remove();
    });
}

function adsRemover() {
    $('body > div:last-child').remove();
}