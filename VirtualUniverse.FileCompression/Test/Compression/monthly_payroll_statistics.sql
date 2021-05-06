/*
 Navicat Premium Data Transfer

 Source Server         : 192.168.3.104
 Source Server Type    : MySQL
 Source Server Version : 50731
 Source Host           : 192.168.3.104:13314
 Source Schema         : th_bank_account

 Target Server Type    : MySQL
 Target Server Version : 50731
 File Encoding         : 65001

 Date: 29/03/2021 10:28:07
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for monthly_payroll_statistics
-- ----------------------------
DROP TABLE IF EXISTS `monthly_payroll_statistics`;
CREATE TABLE `monthly_payroll_statistics`  (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `month` date NULL DEFAULT NULL COMMENT '统计年月',
  `project_id` bigint(20) NULL DEFAULT NULL COMMENT '项目Id',
  `total_pay_amount` bigint(255) NULL DEFAULT NULL COMMENT '应发金额',
  `actual_pay_amount` bigint(255) NULL DEFAULT NULL COMMENT '实发金额',
  `create_name` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '创建人;如果是系统内部用户，则记录用户id;如果是系统内部用户，则记录用户id；如果是外部接口传入，则记录数据来源',
  `create_date` datetime(6) NOT NULL COMMENT '创建时间;取数据库当前时间',
  `update_name` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '编辑人;如果是系统内部用户，则记录用户id；如果是外部接口传入，则记录数据来源',
  `update_date` datetime(6) NULL DEFAULT NULL COMMENT '编辑时间;取数据库当前时间',
  `delete_name` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '删除人;删除标记=Y时必填；如果是系统内部用户，则记录用户id；如果是外部接口传入，则记录数据来源;',
  `delete_date` datetime(6) NULL DEFAULT NULL COMMENT '删除时间;删除标记=Y时必填；取数据库当前时间;',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '月度工资统计' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
