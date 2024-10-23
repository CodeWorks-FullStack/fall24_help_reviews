CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) UNIQUE COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS restaurants (
    id int not null primary key AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    name VARCHAR(255) not null,
    description VARCHAR(255) not null,
    imgUrl VARCHAR(255) not null,
    visits int not null default 0,
    isShutdown BOOLEAN not null default false,
    creatorId VARCHAR(255) not null,
    Foreign Key (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

drop table restaurants;

CREATE TABLE IF NOT EXISTS restaurant_reviews (
    id int not null primary key AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title VARCHAR(255) not null,
    body VARCHAR(255) not null,
    imgUrl VARCHAR(255) not null,
    creatorId VARCHAR(255) not null,
    restaurantId int not null,
    Foreign Key (creatorId) REFERENCES accounts (id) on delete CASCADE,
    Foreign Key (restaurantId) REFERENCES restaurants (id) on delete CASCADE,
    UNIQUE (creatorId, restaurantId)
);

drop table restaurant_reviews;

INSERT INTO
    restaurants (
        name,
        description,
        imgUrl,
        creatorId
    )
VALUES (
        'Chucky Cheese-its',
        'Super cheesy crackers',
        'https://www.foodandwine.com/thmb/hMnLE5x_SXJGpQIP8qAkOAEaUBE=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/Taco-Bell-Adds-2-New-Cheez-It-Menu-Items-FT-BLOG0524-10-96ef8c854ae6451dbea6845b65f0fa18.jpg',
        '6615af310c551fceca365392'
    );

ALTER TABLE restaurant_reviews MODIFY COLUMN body TEXT not null;

INSERT INTO
    restaurant_reviews (
        title,
        body,
        imgUrl,
        restaurantId,
        creatorId
    )
VALUES (
        'THE BEST PLACE FOR YOUR CHEESY NEEDS!!!',
        'Cheesy feet blue castello cheese on toast. Smelly cheese danish fontina gouda squirty cheese stinking bishop cheese and biscuits cheeseburger cauliflower cheese. Cheese triangles danish fontina monterey jack cottage cheese fromage cheeseburger babybel feta. Melted cheese.',
        'https://townsquare.media/site/658/files/2023/11/attachment-meltz-birthday.jpg?w=780&q=75',
        1,
        '6615af310c551fceca365392'
    );

INSERT INTO
    restaurant_reviews (
        title,
        body,
        imgUrl,
        restaurantId,
        creatorId
    )
VALUES (
        'Meh, it was okay.',
        'The cheese does melt.',
        'https://media1.popsugar-assets.com/files/thumbor/ACOvbXq39X03qgEqhiwGA7gn_bw=/fit-in/792x792/filters:format_auto():upscale()/2018/04/22/171/n/1922195/a330a3315add4dde8f3eb8.49413590_.jpg',
        1,
        '6615af3118b6519506ebd7e1'
    );

SELECT restaurants.*, COUNT(
        restaurant_reviews.`restaurantId`
    ) AS reviewCount, accounts.*
FROM
    restaurants
    JOIN accounts ON accounts.id = restaurants.`creatorId`
    -- REVIEW Include data even if its null
    LEFT OUTER JOIN restaurant_reviews ON restaurant_reviews.`restaurantId` = restaurants.id
GROUP BY
    restaurants.`id`;


UPDATE restaurants SET visits = visits + 1 WHERE restaurants.id = 1;
SELECT * FROM restaurants
JOIN accounts ON accounts.id = restaurants.creatorId
WHERE restaurants.id = 1