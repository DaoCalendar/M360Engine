(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', getRoutes());

    // Configure the routes and route resolvers
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {

        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    }

    // Define the routes 
    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: 'app/home/home.html',
                    title: 'home',
                    settings: {
                        nav: 1,
                        content: '<img class="sidebarImage" src="Content/images/home.png" /> Home'
                    }
                }
            }, {
                url: '/advancedSearch',
                config: {
                    title: 'advancedSearch',
                    templateUrl: 'app/advancedSearch/advancedSearch.html',
                    settings: {
                        nav: 2,
                        content: '<img class="sidebarImage" src="Content/images/advanced_search.png" /> Advanced Search'
                    }
                }
            }, {
                url: '/sales',
                config: {
                    title: 'sales',
                    templateUrl: 'app/sales/sales.html',
                    settings: {
                        nav: 3,
                        content: '<img class="sidebarImage" src="Content/images/sales.png" /> Sales'
                    }
                }
            }, {
                url: '/procurement',
                config: {
                    title: 'procurement',
                    templateUrl: 'app/procurement/procurement.html',
                    settings: {
                        nav: 4,
                        content: '<img class="sidebarImage" src="Content/images/procurement.png" /> Procurement'
                    }
                }
            }, {
                url: '/customers',
                config: {
                    title: 'customers',
                    templateUrl: 'app/customers/customers.html',
                    settings: {
                        nav: 5,
                        content: '<img class="sidebarImage" src="Content/images/customers.png" /> Customers'
                    }
                }
            }, {
                url: '/tickets',
                config: {
                    title: 'tickets',
                    templateUrl: 'app/tickets/tickets.html',
                    settings: {
                        nav: 6,
                        content: '<img class="sidebarImage" src="Content/images/tickets.png" /> Tickets'
                    }
                }
            }, {
                url: '/knowledgebase',
                config: {
                    title: 'knowledgebase',
                    templateUrl: 'app/knowledgebase/knowledgebase.html',
                    settings: {
                        nav: 7,
                        content: '<img class="sidebarImage" src="Content/images/knowledgebase.png" /> Knowledgebase'
                    }
                }
            }, {
                url: '/bestPractices',
                config: {
                    title: 'bestPractices',
                    templateUrl: 'app/bestPractices/bestPractices.html',
                    settings: {
                        nav: 8,
                        content: '<img class="sidebarImage" src="Content/images/best_practices.png" /> Best Practices'
                    }
                }
            }, {
                url: '/humanResources',
                config: {
                    title: 'humanResources',
                    templateUrl: 'app/humanResources/humanResources.html',
                    settings: {
                        nav: 9,
                        content: '<img class="sidebarImage" src="Content/images/human_resources.png" /> Human Resources'
                    }
                }
            }, {
                url: '/finance',
                config: {
                    title: 'finance',
                    templateUrl: 'app/finance/finance.html',
                    settings: {
                        nav: 10,
                        content: '<img class="sidebarImage" src="Content/images/finance.png" /> Finance'
                    }
                }
            }, {
                url: '/management',
                config: {
                    title: 'management',
                    templateUrl: 'app/management/management.html',
                    settings: {
                        nav: 11,
                        content: '<img class="sidebarImage" src="Content/images/management.png" /> Management'
                    }
                }
            }
        ];
    }
})();